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
    }
}
