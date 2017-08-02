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

        RunasContext db = new RunasContext();

        public ActionResult Index()
        {
            List<Producto> Productos = db.Productos.Include("Categorias").Include("Autores").AsQueryable().OrderByDescending(a => a.Fecha).Take(20).ToList();
            ViewBag.Productos = Productos;
            return View();
        }

        public ActionResult Producto (int prodID)
        {
            Producto Producto = db.Productos.Include("Categorias").Include("Autores").AsQueryable().Single(a => a.ID == prodID);
            List<Resenha> Resenhas = db.Resenhas.Include("Autor").Where(cm => cm.ProductoResenhado.ID == prodID).OrderByDescending(cm => cm.Fecha).ToList();
            ViewBag.Producto = Producto;
            ViewBag.Resenhas = Resenhas;

            ViewBag.Title = "Product: " + Producto.Titulo;
            return View();
        }

        /// <summary>
        /// Método a usar por Ajax que agrega productos al carrito de compras del usuario y los pasa a la Sesión.
        /// </summary>
        /// <param name="prodID">ID del producto</param>
        public void AgregarACarrito (int prodID)
        {
            Producto ProductoAAgregar = db.Productos.FirstOrDefault(pr => pr.ID == prodID);

            if (((List<CarritoDeCompras>)Session["Carrito"]) != null && ((List<CarritoDeCompras>)Session["Carrito"]).Any(CdC => CdC.Producto.ID == prodID))
            {
                ((List<CarritoDeCompras>)Session["Carrito"]).First(CdC => CdC.Producto.ID == prodID).Cantidad += 1;
            } else if (((List<CarritoDeCompras>)Session["Carrito"]) != null)
            {
                CarritoDeCompras NuevoProducto = new CarritoDeCompras(ProductoAAgregar);
                ((List<CarritoDeCompras>)Session["Carrito"]).Add(NuevoProducto);
            } else
            {
                CarritoDeCompras NuevoProducto = new CarritoDeCompras(ProductoAAgregar);
                List<CarritoDeCompras> NuevoCarrito = new List<CarritoDeCompras>();
                NuevoCarrito.Add(NuevoProducto);
                Session["Carrito"] = NuevoCarrito;
            }
        }
    }
}