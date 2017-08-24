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
    }
}