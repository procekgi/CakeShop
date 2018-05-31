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
            var lst = new PedidoDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult CadastroPedido()
        {
            return View();
        }

        public ActionResult SalvarPedido(Pedido obj)
        {
            new PedidoDAO().Inserir(obj);
            return RedirectToAction("", "");
        }

        public ActionResult Detalhes(int id)
        {
            var pedido = new PedidoDAO().BuscarPorId(id);
            pedido.Itens = new ItemPedidoDAO().BuscarPorPedido(id);
            return View(pedido);
        }
    }
}