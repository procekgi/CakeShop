using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class ContatoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enviar(Contato obj)
        {
            //new ContatoDAO().Inserir(obj);
            return RedirectToAction("Index", "Contato");
        }
    }
}