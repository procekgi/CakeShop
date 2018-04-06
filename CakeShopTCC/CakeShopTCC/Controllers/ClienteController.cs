using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult TabelaClientes()
        {
            var lstClientes = new List<Cliente>();
            return View(lstClientes);
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Cadastrar(Cliente obj)
        {
            new ClienteDAO().Inserir(obj);
            return RedirectToAction("TabelaClientes", "Usuario");
        }

        public ActionResult MinhaConta()
        {
            return View();
        }
    }
}