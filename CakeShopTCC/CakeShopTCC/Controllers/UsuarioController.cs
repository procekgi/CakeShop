using CakeShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TabelaProdutosDoces()
        {
            return View();
        }

        public ActionResult TabelaProdutosSalgados()
        {
            return View();
        }

        public ActionResult TabelaProdutosBolos()
        {
            return View();
        }
        public ActionResult TabelaClientes()
        {
            var lstClientes = new ClienteDAO().BuscarTodos();
            return View(lstClientes);
        }
    }
}