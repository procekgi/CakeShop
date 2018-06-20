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
        public Cliente UsuarioLogado { get; private set; }

        public ActionResult Index()
        {
            var lst = new PedidoDAO().BuscarTodos();
            lst.ForEach(p =>
            {
                p.Itens = new ItemPedidoDAO().BuscarPorPedido(p.Id_Pedido);
            });
            return View(lst);
        }

        public ActionResult VisualizarItens(int id)
        {
            var pedido = new ItemPedidoDAO().BuscarPorId(id);
            pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(id);
            return View(pedido);
        }

        //public ActionResult CadastroPedido()
        //{
        //    return View();
        //}

        public ActionResult Finalizar(Pedido obj)
        {
            obj.Cliente = UsuarioLogado;
            //new PedidoDAO().Inserir(obj);
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