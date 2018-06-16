using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CakeShop.Models;
using CakeShop.DataAccess;
using CakeShopTCC;

namespace CakeShopTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Usuario u = new Usuario();
            u.Login = "admin";
            u.Senha = "123";

            Assert.AreEqual(u.Login, "admin");
        }

        [TestMethod]
        public void LoginTestfalse()
        {
            Usuario u = new Usuario();
            u.Login = "admin";
            u.Senha = "123";

            Assert.AreEqual(u.Senha, "administrador123");
        }

        [TestMethod]
        public void TesteItemPedido()
        {
            ItemPedido ip = new ItemPedido();
            ip.Pedido.Id_Pedido = 1;
            //ip.Produto = "Brigadeiro"; //popular o produto
            ip.Quantidade = (0);
            ip.Preco = new decimal(1.50);
            Assert.AreEqual(ip.Validar, ''); 


            Assert.IsTrue(ip.Quantidade < 0);
        }
    }
}

