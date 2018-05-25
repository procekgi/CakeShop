using CakeShop.DataAccess;
using CakeShop.Models;
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
            var lst = new ProdutoDAO().BuscarPorCategoria(1);
            return View(lst);
        }

        public ActionResult TabelaProdutosSalgados()
        {
            var lst = new ProdutoDAO().BuscarPorCategoria(2);
            return View(lst);
        }

        public ActionResult TabelaProdutosBolos()
        {
            var lst = new ProdutoDAO().BuscarPorCategoria(3);
            return View(lst);
        }

        public ActionResult TabelaClientes()
        {
            var lstClientes = new ClienteDAO().BuscarTodos();
            return View(lstClientes);
        }
    }
}