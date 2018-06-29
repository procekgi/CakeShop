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
        private const string defaultApiKey = "ak_test_RsyGsYkn141xJzJ2v1LwI8f8z9kiih";
        private const string defaultEncryptionKey = "ek_test_HmftBWggaUUkQJrzuBavmhv8WkHUO7";

        public ActionResult Index()
        {
            var lst = new PedidoDAO().BuscarTodos().Where(p => p.Status == STATUS_PEDIDO.PAGAMENTO_REALIZADO).ToList();

            lst.ForEach(p =>
            {
                p.Itens = new ItemPedidoDAO().BuscarPorPedido(p.Id_Pedido);
            });

            return View(lst);
        }

        public ActionResult IndexDetalhes(int id)
        {
            var pedido = new PedidoDAO().BuscarPorId(id);
            pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(id);
            return View(pedido);
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

        private bool ValidarDataEntrega(DateTime? dataEntrega)
        {
            //deve ter uma data de entrega preenchida na tela
            //e a data de entrega deve ser maior que a data e hora atual
            if (dataEntrega.HasValue && dataEntrega > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ActionResult Finalizar(Pedido obj)
        {
            var pedido = new PedidoDAO().BuscarPorId(obj.Id_Pedido);
            if (pedido != null)
            {
                pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(obj.Id_Pedido);
            }

            if (pedido != null && !pedido.DataEntrega.HasValue)
            {
                //validação da data de entrega do pedido
                if (!ValidarDataEntrega(obj.DataEntrega))
                {
                    ViewBag.ErroMsg = @"Data de entrega inválida, por favor insira uma data válida maior que o dia de hoje!";
                    return View("Detalhes", pedido);
                }

                //atualizando o campo data entrega que foi preenchido na tela de detalhes do pedido
                new PedidoDAO().AtualizarDataEntrega(obj);
            }

            //redicionando para a tela de pagamento
            return RedirectToAction("Pagamento", "Pedido", new { id = obj.Id_Pedido });
        }

        public ActionResult Pagamento(int id)
        {
            var pedido = new PedidoDAO().BuscarPorId(id);
            pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(id);
            return View(pedido);
        }

        public ActionResult FazerPagto(Pedido obj)
        {
            //chamando API de pagamento "pagar.me" para liberar ou recusar o pagamento
            try
            {
                var pedido = new PedidoDAO().BuscarPorId(obj.Id_Pedido);
                pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(obj.Id_Pedido);
                pedido.NumeroCartao = obj.NumeroCartao;
                pedido.NomeDoTitular = obj.NomeDoTitular;
                pedido.DataValidade = obj.DataValidade;
                pedido.CodigoVerificacao = obj.CodigoVerificacao;

                PagarMeService service = new PagarMeService(defaultApiKey, defaultEncryptionKey);

                CardHash card = new CardHash(service);
                card.CardNumber = obj.NumeroCartao;
                card.CardHolderName = obj.NomeDoTitular;
                card.CardExpirationDate = obj.DataValidade;
                card.CardCvv = obj.CodigoVerificacao;

                string cardHash = card.Generate();

                Transaction transaction = new Transaction(service);

                transaction.Amount = (int)(pedido.ValorTotal * 100);
                transaction.CardHash = cardHash;
                transaction.PaymentMethod = PaymentMethod.CreditCard;

                pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(pedido.Id_Pedido);
                var lst = new List<Item>();

                foreach (var item in pedido.Itens)
                {
                    lst.Add(new Item()
                    {
                        Id = item.Id.ToString(),
                        Title = item.Produto.Nome_Produto,
                        Quantity = item.Quantidade,
                        Tangible = true,
                        UnitPrice = Convert.ToInt32(item.Preco * 100m)
                    });
                }

                transaction.Item = lst.ToArray();
                transaction.Save();

                TransactionStatus status = transaction.Status;

                if (status == TransactionStatus.Paid)
                {
                    pedido.Status = STATUS_PEDIDO.PAGAMENTO_REALIZADO;
                    new PedidoDAO().AtualizarStatus(pedido);
                }
                else
                {
                    pedido.Status = STATUS_PEDIDO.PAGAMENTO_RECUSADO;
                    new PedidoDAO().AtualizarStatus(pedido);
                }
            }
            catch (PagarMeException ex)
            {
                var pedido = new PedidoDAO().BuscarPorId(obj.Id_Pedido);
                pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(obj.Id_Pedido);
                ViewBag.ErroMsg = ex.Message;
                return View("Pagamento", pedido);
            }

            return RedirectToAction("MeusPedidos", "Cliente");
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