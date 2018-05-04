using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult CadastroProduto()
        {
            ViewBag.Unidades = new UnidadeDeMedidaDAO().BuscarTodos();
            ViewBag.Categorias = new CategoriaDAO().BuscarTodos();
            return View();
        }

        public ActionResult PaginaDoces()
        {
            return View();
        }

        public ActionResult PaginaSalgados()
        {
            return View();
        }

        public ActionResult PaginaBolos()
        {
            return View();
        }

        public ActionResult ListaTodosOsProdutos()
        {
            var lst = new ProdutoDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult SalvarProduto(Produto obj)
        {
            new ProdutoDAO().Inserir(obj);

            switch (obj.Categoria.Id_Categoria)
            {
                case 1:
                    return RedirectToAction("TabelaProdutosDoces", "Usuario");
                case 2:
                    return RedirectToAction("TabelaProdutosSalgados", "Usuario");

                case 3:
                    return RedirectToAction("TabelaProdutosBolos", "Usuario");

                default:
                    return RedirectToAction("ListaTodosOsProdutos", "Produto");
            }

            
        }



        //private readonly ModelDb db;
        //public ProdutoController()
        //{
        //    db.Dispose();
        //}


        //[HttpGet]
        //public ActionResult PesquisarProduto()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult PesquisarProduto(string texto)
        //{
        //    return View(db.)
        //}
             
    }

   
}