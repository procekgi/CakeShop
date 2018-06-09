using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CakeShop.Models;
using CakeShop.DataAccess;
namespace CakeShopTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            Confeiteira c = new Confeiteira();
            c.Email = "procekgi@gmail.com";
            c.Nome = "Giovana Machado";
            c.Login = "ConfeiteiraLog";
            c.Senha = "cs_giovana";
                                     
                
        }
       
               
    }
}
