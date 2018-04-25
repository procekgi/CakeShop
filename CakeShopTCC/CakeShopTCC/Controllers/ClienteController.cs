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

        public ActionResult Entrar(Cliente obj)
        {
            var usuarioLogado = new ClienteDAO().Logar(obj);

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

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthenticationUtil.SignOut();

            return RedirectToAction("Cadastro", "Cliente");
        }


        public ActionResult MeusPedidos()
        {
            return View();
        }
    }
}