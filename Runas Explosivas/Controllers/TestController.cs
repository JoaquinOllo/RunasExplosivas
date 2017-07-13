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
        public string PruebaEF()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.Mail = "larala@gmail.com";
                nuevoUsuario.Nombre = "Lara La";
                nuevoUsuario.Password = "123456";
                nuevoUsuario.ImagenDePerfil = "";
                nuevoUsuario.IsAdmin = false;

                db.Usuarios.Add(nuevoUsuario);
                db.SaveChanges();
            }
            return "listo";
        }
    }
}