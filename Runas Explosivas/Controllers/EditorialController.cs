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

        public ActionResult Checkout ()
        {
            if (Session["Usuario"] != null)
            {
                return View();
            } else
            {
                TempData["Reporte"] = "Debes conectarte con tu cuenta de usuario registrarte antes de proceder con la confirmación de la compra";
                TempData["TipoDeReporte"] = "danger";
                return RedirectToAction("Index", "Editorial");
            }
        }

        /// <summary>
        /// Método a usar por Ajax que agrega productos al carrito de compras del usuario y los pasa a la Sesión.
        /// </summary>
        /// <param name="prodID">ID del producto</param>
        public ActionResult AgregarACarrito (int prodID)
        {
            Producto ProductoAAgregar = db.Productos.FirstOrDefault(pr => pr.ID == prodID);

            if (Session["Carrito"] != null && ((List<ProductoEnCarrito>)Session["Carrito"]).Any(CdC => CdC.Producto.ID == prodID))
            {
                ((List<ProductoEnCarrito>)Session["Carrito"]).First(CdC => CdC.Producto.ID == prodID).Cantidad += 1;
            } else if (((List<ProductoEnCarrito>)Session["Carrito"]) != null)
            {
                ProductoEnCarrito NuevoProducto = new ProductoEnCarrito(ProductoAAgregar);
                ((List<ProductoEnCarrito>)Session["Carrito"]).Add(NuevoProducto);
            } else
            {
                ProductoEnCarrito NuevoProducto = new ProductoEnCarrito(ProductoAAgregar);
                List<ProductoEnCarrito> NuevoCarrito = new List<ProductoEnCarrito>();
                NuevoCarrito.Add(NuevoProducto);
                Session["Carrito"] = NuevoCarrito;
            }
            return RedirectToAction("GetCarrito", "Editorial");
        }

        public ActionResult EliminarDeCarrito(int prodID)
        {
            ProductoEnCarrito ProductoAEliminar = ((List<ProductoEnCarrito>)Session["Carrito"]).First(CdC => CdC.Producto.ID == prodID);

            if (Session["Carrito"] != null && ((List<ProductoEnCarrito>)Session["Carrito"]).Any(CdC => CdC.Producto == ProductoAEliminar.Producto))
            {
                ProductoAEliminar.Cantidad -= 1;
                if (ProductoAEliminar.Cantidad == 0)
                {
                    ((List<ProductoEnCarrito>)Session["Carrito"]).Remove(ProductoAEliminar);
                }
            }

            return RedirectToAction("GetCarrito", "Editorial");
        }

        public JsonResult GetCarrito ()
        {
            if (Session["Carrito"] != null)
            {
                var CarritoFinal = ((List<ProductoEnCarrito>)Session["Carrito"]).Select(C => new {
                    ID = C.Producto.ID,
                    Titulo = C.Producto.Titulo,
                    Cantidad = C.Cantidad,
                    Precio = C.Producto.Precio * C.Cantidad});
                    return Json(CarritoFinal, JsonRequestBehavior.AllowGet);
            } else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult VaciarCarrito()
        {
            Session["Carrito"] = null;
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}