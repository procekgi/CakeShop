using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CakeShopTCC.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrar(Confeiteira obj)
        {
            var usuarioLogado = new UsuarioDAO().Logar(obj);

            if (usuarioLogado == null)
            {
                return RedirectToAction("Cadastro", "Cliente");
            }

            var userData = new JavaScriptSerializer().Serialize(new Usuario()
            {
                Id = usuarioLogado.Id,
                Nome = usuarioLogado.Nome,
                Login = usuarioLogado.Email
            });

            FormsAuthenticationUtil.SetCustomAuthCookie(usuarioLogado.Login, userData, false);

            return RedirectToAction("Index", "Pedido");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Index", "Login");
        }
    }
}