﻿using CakeShop.DataAccess;
using CakeShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeShopTCC.Controllers
{
    public class ProdutoController : Controller
    {
        public ActionResult CadastroProduto()
        {

            if(!string.IsNullOrWhiteSpace(ViewBag.Error))
            {
                ViewBag.Error = @"Campo vazio. Preencha todos os campos!";
                return View();
            }
            else
            {
                ViewBag.Unidades = new UnidadeDeMedidaDAO().BuscarTodos();
                ViewBag.Categorias = new CategoriaDAO().BuscarTodos();
                return View();
            }
        }

        public ActionResult PaginaDoces()
        {
            var lst = new ProdutoDAO().BuscarTodos().Where(p => p.Categoria.Nome_Categoria.ToUpper() == "DOCES").ToList();
            return View(lst);
        }

        public ActionResult PaginaSalgados()
        {
            var lst = new ProdutoDAO().BuscarTodos().Where(p => p.Categoria.Nome_Categoria.ToUpper() == "SALGADOS").ToList();
            return View(lst);
        }

        public ActionResult PaginaBolos()
        {
            var lst = new ProdutoDAO().BuscarTodos().Where(p => p.Categoria.Nome_Categoria.ToUpper() == "BOLOS").ToList();
            return View(lst);
        }

        public ActionResult ListaTodosOsProdutos()
        {
            var lst = new ProdutoDAO().BuscarTodos();
            return View(lst);
        }

        public ActionResult SalvarProduto(Produto obj)
        {
            new ProdutoDAO().Inserir(obj);

            switch (obj.Categoria.Id_Categoria)
            {
                case 1:
                    return RedirectToAction("TabelaProdutosDoces", "Usuario");
                case 2:
                    return RedirectToAction("TabelaProdutosSalgados", "Usuario");

                case 3:
                    return RedirectToAction("TabelaProdutosBolos", "Usuario");

                default:
                    return RedirectToAction("ListaTodosOsProdutos", "Produto");
            }
        }

        [HttpPost]
        public JsonResult Upload()
        {
            try
            {
                if (!Directory.Exists(Server.MapPath("~/Uploads")))
                    Directory.CreateDirectory(Server.MapPath("~/Uploads"));

                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fileName];
                    string savedFileName = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(f.FileName));
                    FileInfo fi = new FileInfo(savedFileName);
                    f.SaveAs(savedFileName);

                    return Json(fi.Name);
                }

                return Json(null);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }
    }
}