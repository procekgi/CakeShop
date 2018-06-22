using CakeShop.DataAccess;
using CakeShop.Models;
using PagarMe;
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
            try
            {
                var defaultApiKey = "ak_test_RsyGsYkn141xJzJ2v1LwI8f8z9kiih";
                var defaultEncryptionKey = "ek_test_HmftBWggaUUkQJrzuBavmhv8WkHUO7";

                PagarMeService service = new PagarMeService(defaultApiKey, defaultEncryptionKey);

                CardHash card = new CardHash(service);
                card.CardNumber = "41111111111111111";
                card.CardHolderName = "tiago Andrade";
                card.CardExpirationDate = "1022";
                card.CardCvv = "123";

                string cardHash = card.Generate();

                Transaction transaction = new Transaction(service);

                transaction.Amount = (int)(199.87 * 100);
                transaction.CardHash = cardHash;
                transaction.PaymentMethod = PaymentMethod.CreditCard;

                transaction.Item = new[]
                {
                    new Item()
                    {
                        Id = "1",
                        Title = "Little Car",
                        Quantity = 1,
                        Tangible = true,
                        UnitPrice = 100*100
                    },
                    new Item()
                    {

                        Id = "2",
                        Title = "Baby Crib",
                        Quantity = 1,
                        Tangible = true,
                        UnitPrice = (int)(99.87*100)
                    }
                };

                transaction.Save();

                TransactionStatus status = transaction.Status;
            }
            catch (PagarMeException ex)
            {
                throw ex;
            }

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