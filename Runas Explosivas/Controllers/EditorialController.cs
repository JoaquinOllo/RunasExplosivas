using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            if (Session["Carrito"] != null && ((List<ProductoEnCarro>)Session["Carrito"]).Any(CdC => CdC.Producto.ID == prodID))
            {
                ((List<ProductoEnCarro>)Session["Carrito"]).First(CdC => CdC.Producto.ID == prodID).Cantidad += 1;
            } else if (((List<ProductoEnCarro>)Session["Carrito"]) != null)
            {
                ProductoEnCarro NuevoProducto = new ProductoEnCarro(ProductoAAgregar);
                ((List<ProductoEnCarro>)Session["Carrito"]).Add(NuevoProducto);
            } else
            {
                ProductoEnCarro NuevoProducto = new ProductoEnCarro(ProductoAAgregar);
                List<ProductoEnCarro> NuevoCarrito = new List<ProductoEnCarro>();
                NuevoCarrito.Add(NuevoProducto);
                Session["Carrito"] = NuevoCarrito;
            }
            return RedirectToAction("GetCarrito", "Editorial");
        }

        public ActionResult EliminarDeCarrito(int prodID)
        {
            ProductoEnCarro ProductoAEliminar = ((List<ProductoEnCarro>)Session["Carrito"]).First(CdC => CdC.Producto.ID == prodID);

            if (Session["Carrito"] != null && ((List<ProductoEnCarro>)Session["Carrito"]).Any(CdC => CdC.Producto == ProductoAEliminar.Producto))
            {
                ProductoAEliminar.Cantidad -= 1;
                if (ProductoAEliminar.Cantidad == 0)
                {
                    ((List<ProductoEnCarro>)Session["Carrito"]).Remove(ProductoAEliminar);
                }
            }

            return RedirectToAction("GetCarrito", "Editorial");
        }

        public JsonResult GetCarrito ()
        {
            if (Session["Carrito"] != null)
            {
                var CarritoFinal = ((List<ProductoEnCarro>)Session["Carrito"]).Select(C => new {
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

        public ActionResult RegistrarCompra(string inputFormaDePago)
        {
            if (Session["Usuario"]!= null & Session["Carrito"] != null)
            {
                /* EXTRAEMOS DATOS DE LA SESSION PARA EVITAR CONFLICTOS DE CONTEXTOS */
                string UsuarioMail = ((Usuario)Session["Usuario"]).Mail;
                int DatosDeEnvioID = ((DatosDeEnvio)Session["DatosDeEnvio"]).ID;
                DatosDeEnvio DatosDeEnvio = db.TablaDatosDeEnvio.First(DE => DE.ID == DatosDeEnvioID);

                /* CREAMOS UNA COLECCIÓN DE PRODUCTOSENCARRITO NUEVA, PARA PASAR LOS PRODUCTOS 
                 * DE LA SESIÓN A UNA VARIABLE DISTINTA, DE VUELTA PARA EVITAR CONFLICTOS DE CONTEXTOS */ 

                ICollection<ProductoEnCarro> ProductosComprados = new List<ProductoEnCarro>() { };
                foreach (ProductoEnCarro ProductoEnCarrito in (ICollection<ProductoEnCarro>)Session["Carrito"])
                {
                    while (true)
                    {
                        ProductoEnCarro ConsultaProductoABD = db.ProductosEnCarro.AsQueryable().Include(P => P.Producto).FirstOrDefault(
                                            PR => PR.Cantidad == ProductoEnCarrito.Cantidad
                                            && PR.Producto.ID == ProductoEnCarrito.Producto.ID);
                        if (ConsultaProductoABD != default(ProductoEnCarro))
                        {
                            ProductosComprados.Add(ConsultaProductoABD);
                            break;
                        } else
                        {
                            Producto Producto = db.Productos.First(P => P.ID == ProductoEnCarrito.Producto.ID);
                            ProductoEnCarro ProductoNuevo = new ProductoEnCarro(Producto, ProductoEnCarrito.Cantidad);
                            db.ProductosEnCarro.Add(ProductoNuevo);
                            db.SaveChanges();
                        }
                    }
                }

                Compra NuevaCompra = new Compra()
                {
                    Comprador = db.Usuarios.First(U => U.Mail == UsuarioMail),
                    Productos = ProductosComprados,
                    Fecha = DateTime.Now,
                    MailEnviado = false,
                    CompraAbonada = false,
                    FechaEntregaEstimada = new DateTime(2005, 1, 1)
                };

                if (!((ICollection<ProductoEnCarro>)Session["Carrito"]).All(Pr => Pr.Producto.Categorias.Any(C => C.Nombre == "digital")))
                {
                    NuevaCompra.DatosDeEnvio = DatosDeEnvio;
                }
                if (NuevaCompra.PrecioTotal > 0)
                {
                    NuevaCompra.FormaDePago = inputFormaDePago;
                }
                db.Compras.Add(NuevaCompra);
                db.SaveChanges();
                TempData["Reporte"] = "Tu compra fue registrada con éxito! Nos pondremos en contacto en breve, revisa tu bandeja de entrada! " +
                    "Puedes revisar el estado de tu orden desde el botón de Usuario de la barra lateral. Muchas gracias!";
                TempData["TipoDeReporte"] = "success";
            }

            return RedirectToAction("Index", "Editorial");
        }
    }
}