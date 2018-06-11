using CakeShop.DataAccess;
using CakeShop.Models;
using System.Collections.Generic;
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
            //usuario logado no sistema (pegando o id)
            var id = ((Usuario)User).Id;
            //buscar no banco de dados, os todos os dados do usuario logado no sistema
            var cliente = new ClienteDAO().BuscarPorId(id);
            return View(cliente);
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
            if (HttpContext.User == null || HttpContext.User.GetType() != typeof(Usuario))
                return RedirectToAction("Cliente", "Cadastro");

            var pedidos = new PedidoDAO().BuscarTodosPorCliente(new Cliente() { Id = ((Usuario)User).Id });
            pedidos.ForEach(p =>
            {
                p.Itens = new ItemPedidoDAO().BuscarPorPedido(p.Id_Pedido);
            });
            return View(pedidos);
        }
    }
}