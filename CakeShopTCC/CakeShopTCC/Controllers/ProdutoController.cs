using CakeShop.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult CadastroProduto()
        {
            return View();
        }

        public ActionResult Doces()
        {
            return View();
        }
        public ActionResult Salgados()
        {
            return View();
        }
        public ActionResult Bolos()
        {
            return View();
        }
        public ActionResult ListaTodosOsProdutos()
        {
            var lst = new ProdutoDAO().BuscarTodos();
            return View(lst);
        }



    }
}