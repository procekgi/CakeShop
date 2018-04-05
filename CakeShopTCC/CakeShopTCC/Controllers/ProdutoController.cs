﻿using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult CadastroProduto()
        {
            ViewBag.Unidades = new UnidadeDeMedidaDAO().BuscarTodos();
            ViewBag.Categorias = new CategoriaDAO().BuscarTodos();
            return View();
        }

        public ActionResult Doces()
        {
            return View();
        }

        public ActionResult Salgados()
        {
            return View();
        }

        public ActionResult Bolos()
        {
            return View();
        }

        public ActionResult ListaTodosOsProdutos()
        {
            var lst = new ProdutoDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult SalvarProduto(Produto obj)
        {
            new ProdutoDAO().Inserir(obj);
            return RedirectToAction("ListaTodosOsProdutos", "Produto");
        }
    }
}