using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class UnidadeDeMedidaController : Controller
    {
        // GET: UnidadeDeMedida
        public ActionResult Index()
        {
            var lst = new UnidadeDeMedidaDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult Salvar(UnidadeDeMedida obj)
        {
            new UnidadeDeMedidaDAO().Inserir(obj);
            return RedirectToAction("Index","UnidadeDeMedida");
        }

        public ActionResult CadastroUnidadeDeMedida()
        {
            return View();
        }
    }
}