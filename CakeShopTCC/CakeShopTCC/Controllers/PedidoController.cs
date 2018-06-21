using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class PedidoController : Controller
    {
        public ActionResult Index()
        {
            var lst = new PedidoDAO().BuscarTodos().Where(p => p.Status == STATUS_PEDIDO.PAGAMENTO_REALIZADO).ToList();
            lst.ForEach(p =>
            {
                p.Itens = new ItemPedidoDAO().BuscarPorPedido(p.Id_Pedido);
            });
            return View(lst);
        }

        public ActionResult VisualizarItens(int id)
        {
            var pedido = new PedidoDAO().BuscarPorId(id);
            pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(id);
            return View(pedido);
        }

        public ActionResult Entregar(int id)
        {
            //primeiro eu busco o pedido pelo id no banco de dados
            var pedido = new PedidoDAO().BuscarPorId(id);

            //atualiza o status do pedido para finalizado
            new PedidoDAO().Entregar(pedido);

            //redirecionando para a tela de listagem de pedidos da confeiteira
            return RedirectToAction("Index", "Pedido");
        }

        public ActionResult Finalizar(Pedido obj)
        {
            //atualizando o campo data entrega que foi preenchido na tela de detalhes do pedido
            new PedidoDAO().Atualizar(obj);

            //redicionando para a tela de pagamento
            return RedirectToAction("Pagamento", "Pedido");
        }

        public ActionResult Pagamento()
        {
            return View();
        }

        public ActionResult Detalhes(int id)
        {
            var pedido = new PedidoDAO().BuscarPorId(id);
            pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(id);
            return View(pedido);
        }

        public ActionResult ExcluirItem(int id)
        {
            var item = new ItemPedidoDAO().BuscarPorId(id);
            new ItemPedidoDAO().Excluir(item);
            return RedirectToAction("Detalhes", "Pedido", new { id = item.Pedido.Id_Pedido });
        }

        [HttpPost]
        public JsonResult Comprar(int id, int qtd)
        {
            if (HttpContext.User == null || HttpContext.User.GetType() != typeof(Usuario))
                return Json(Url.Content("~/Cliente/Cadastro"));

            var produto = new ProdutoDAO().BuscarPorId(id);
            if (produto != null)
            {
                var clienteLogado = new Cliente() { Id = ((Usuario)User).Id };
                var pedido = new PedidoDAO().Buscar(clienteLogado, STATUS_PEDIDO.EM_ANDAMENTO);

                if (pedido != null)
                {
                    var item = new ItemPedido();
                    item.Pedido = pedido;
                    item.Produto = produto;
                    item.Quantidade = qtd;
                    item.Preco = produto.Preco;
                    pedido.Itens.Add(item);

                    new ItemPedidoDAO().Inserir(item);
                }
                else
                {
                    pedido = new Pedido();
                    pedido.Status = STATUS_PEDIDO.EM_ANDAMENTO;
                    pedido.Cliente = clienteLogado;
                    pedido.DataPedido = DateTime.Now;

                    new PedidoDAO().Inserir(pedido);

                    var item = new ItemPedido();
                    item.Pedido = pedido;
                    item.Produto = produto;
                    item.Quantidade = qtd;
                    item.Preco = produto.Preco;
                    pedido.Itens.Add(item);

                    new ItemPedidoDAO().Inserir(item);
                }

                return Json(Url.Content(string.Format("~/Pedido/Detalhes/{0}", pedido.Id_Pedido)));
            }

            return Json(Url.Content("~/Home"));
        }
    }
}