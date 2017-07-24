using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class EditorialController : Controller
    {
        // GET: Editorial
        public ActionResult Index()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                List<Producto> Productos = db.Productos.Include("Categorias").Include("Autores").AsQueryable().OrderByDescending(a => a.Fecha).Take(20).ToList();
                ViewBag.Productos = Productos;
                return View();
            }
        }

        public ActionResult Producto (int prodID)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                Producto Producto = db.Productos.Include("Categorias").Include("Autores").AsQueryable().Single(a => a.ID == prodID);
                List<Resenha> Resenhas = db.Resenhas.Include("Autor").Where(cm => cm.ProductoResenhado.ID == prodID).OrderByDescending(cm => cm.Fecha).ToList();
                ViewBag.Producto = Producto;
                ViewBag.Resenhas = Resenhas;

                ViewBag.Title = "Product: " + Producto.Titulo;
            }
                return View();
        }
    }
}