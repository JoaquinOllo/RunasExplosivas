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

        public string PruebaEF2()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                Usuario RunasExplosivas = new Usuario()
                {
                    Mail = "RunasExplosivas@gmail.com",
                    Nombre = "Staff de Runas Explosivas",
                    Password = "RunEx"
                };

                Tag TagPodcast = new Tag("podcast", "headphones", true);

                Articulo nuevoArticulo = new Articulo(
                    "PE nº66: Aplicando Resolución de Conflictos a tu Mesa",
                    new List<Usuario>() { RunasExplosivas },
                    new DateTime(2017, 3, 30, 12, 00, 00),
                    "¡Otro Miércoles de #PodcastExplosivo! Hoy vamos a hablar sobre cómo aplicar lo que aprendimos de juegos con resoluciones de conflicto no binarias a otros juegos más tradicionales. Ideas new school para juegos con raíces en los 80'/90'.",
                    "https://www.youtube.com/watch?v=y-GIvQAsn6w",
                    "",
                    new List<Tag> { TagPodcast }
                    );

                db.Articulos.Add(nuevoArticulo);
                db.SaveChanges();
            }
            return "listo";
        }
    }
}