using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class TestController : Controller
    {
        // GET: Test

        public JsonResult PruebaEF2()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                return Json(((Usuario)Session["Usuario"]).Compras, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PruebaRV(int ArtID)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                return RedirectToAction("Articulo", "Home", new { blogpostID = ArtID });
            }
        }

        public DateTime PrimerArticulo()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                IEnumerable<DateTime> FechasArticulos = db.Articulos.Select(A => A.Fecha);
                int MenorFechaIndex = FechasArticulos.IndexOfMinDate();
                DateTime MenorFecha = db.Articulos.AsEnumerable().ElementAt(MenorFechaIndex).Fecha;
                return MenorFecha;
            }
        }

        public string OrdenarComentarios(int ArtID)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                List<ComentarioEnArticulo> Comentarios = db.ComentariosEnArticulos.Include("ArticuloComentado").Include("RespuestaA")
                    .Where(C => C.ArticuloComentado.ID == ArtID).ToList();
                return LinqExtension.IterarOrdenarComentarios(ArtID);
            }
        }

        public List<DateTime> FiltrarComentarios (int RespuestaAID)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                List<ComentarioEnArticulo> Comentarios = db.ComentariosEnArticulos.Include("ArticuloComentado").Include("RespuestaA")
                    .Where(C => C.ArticuloComentado.ID == 15 && C.RespuestaA.ID == 6).ToList();
                return Comentarios.Select(C => C.Fecha).ToList();
            }
        }
    }
}