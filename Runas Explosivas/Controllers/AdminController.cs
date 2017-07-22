using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }
    }
}