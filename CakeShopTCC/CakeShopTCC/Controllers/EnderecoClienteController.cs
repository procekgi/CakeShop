using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class EnderecoClienteController : Controller
    {
        // GET: EnderecoCliente
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastroEndereco()
        {
            return View();
        }
    }
}