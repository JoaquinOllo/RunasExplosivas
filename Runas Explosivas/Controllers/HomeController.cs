using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool UsuarioRegistrado = false)
        {
            if (UsuarioRegistrado)
            {
                ViewBag.UsuarioRegistrado = true;
            }
            using (RunasContext db = new Models.RunasContext())
            {
                List<Articulo> Articulos = db.Articulos.Include("Tags").AsQueryable().OrderByDescending(u => u.Fecha).Take(20).ToList();
                ViewBag.Articulos = Articulos;
                return View();
            }
        }

        public ActionResult Podcast()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                List<Articulo> ArticulosFiltradosPodcast = db.Articulos.Include("Tags").AsQueryable().Where(a => a.Tags.Any(t => t.Nombre == "podcast")).OrderByDescending(a => a.Fecha).Take(20).ToList();
                ViewBag.Articulos = ArticulosFiltradosPodcast;
                return View();
            }
        }

        public ActionResult Blog()
        {

            using (RunasContext db = new Models.RunasContext())
            {
                List<Articulo> ArticulosFiltradosBlog = db.Articulos.Include("Tags").AsQueryable().Where(a => a.Tags.Any(t => t.Nombre == "blog")).OrderByDescending(a => a.Fecha).Take(20).ToList();
                ViewBag.Articulos = ArticulosFiltradosBlog;
                return View();
            }
        }

        public ActionResult Editorial()
        {
            using (RunasContext db = new Models.RunasContext())
            {
                List<Producto> Productos = db.Productos.Include("Categorias").Include("Autores").AsQueryable().OrderByDescending(a => a.Fecha).Take(20).ToList();
                ViewBag.Productos = Productos;
                return View();
            }
        }

        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult Articulo(int blogpostID)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                Articulo Articulo = db.Articulos.Include("Tags").Include("Autores").AsQueryable().Single(a => a.ID == blogpostID);
                List<ComentarioEnArticulo> Comentarios = db.ComentariosEnArticulos.Include("Autor").Where(cm => cm.ArticuloComentado.ID == blogpostID).OrderByDescending(cm => cm.Fecha).ToList();
                ViewBag.Articulo = Articulo;
                ViewBag.Comentarios = Comentarios;

                if (Articulo.SearchTag("blog"))
                {
                    ViewBag.Title = "Blog";
                    ViewBag.Btn = "btn-lg";
                    ViewBag.BtnUsuario = true;
                    ViewBag.BtnBuscar = true;
                    ViewBag.BtnCarrito = false;
                    ViewBag.HeaderText = ".blog ";
                    ViewBag.HeaderGlyph = "glyphicon glyphicon-pencil";
                } else if (Articulo.SearchTag("podcast"))
                {
                        ViewBag.Title = "Podcast";
                        ViewBag.Btn = "btn-lg";
                        ViewBag.BtnUsuario = true;
                        ViewBag.BtnBuscar = true;
                        ViewBag.BtnCarrito = false;
                        ViewBag.HeaderText = ".podcast ";
                        ViewBag.HeaderGlyph = "glyphicon glyphicon-headphones";
                }
                ViewBag.Title = ViewBag.Title + ": " + Articulo.Titulo;
            }
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult Registate()
        {
            return View();
        }

        public ActionResult Mensaje(string nombre, string email, string mensaje, string SendTo)
        {
            SmtpClient clienteSmtp = new SmtpClient();
            clienteSmtp.Host = "smtp.gmail.com";
            clienteSmtp.Port = 587;
            clienteSmtp.Credentials = new NetworkCredential("joaquinollo@gmail.com", "");
            clienteSmtp.EnableSsl = true;


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("joaquinollo@gmail.com", "Joaquin Ollo");
            mail.To.Add("joaquinollo@gmail.com");
            mail.Subject = "Te contactaron del blog";
            mail.Body = nombre + " (" + email + ") te contactó desde la aplicación, y te dejó el sig. mensaje: " + mensaje;
            clienteSmtp.Send(mail);

            //mail para el usuario
            MailMessage mensajeParaUsuario = new MailMessage();
            mensajeParaUsuario.From = new MailAddress("joaquinollo@gmail.com", "Joaquin Ollo");
            mensajeParaUsuario.To.Add(email);
            mensajeParaUsuario.Subject="Gracias por contactarte con el Blog!";
            mensajeParaUsuario.Body= "Gracias por contactarte con el staff de Runas Explosivas! Tu mensaje será respondido a la brevedad.";

            clienteSmtp.Send(mensajeParaUsuario);
            return View("Index");
        }

        public ActionResult Login(string inputEmail, string inputPassword)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                Usuario UsuarioConectado = db.Usuarios.SingleOrDefault(us => us.Mail == inputEmail && us.Password == inputPassword);

                if (UsuarioConectado == default(Usuario))
                {
                    TempData["Reporte"] = "La dirección ingresada o la contraseña son incorrectas. Por favor, intentá nuevamente";
                    TempData["TipoDeReporte"] = "danger";
                }
                else 
                {
                    Session["Usuario"] = UsuarioConectado;
                    Session["BotonUsuario"] = "borde-azul";
                }
                return RedirectToAction("Index","Home");
            }
        }

        public ActionResult RegistroUsuarioNuevo(string Mail, string Password, string NombreCompleto, string Descripcion)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                if (!db.Usuarios.Any(u => u.Mail == Mail))
                {
                    Usuario UsuarioNuevo = new Usuario()
                    {
                        Mail = Mail,
                        Password = Password,
                        Nombre = NombreCompleto,
                        Descripcion = Descripcion,
                        IsAdmin = false,
                        IsColaborador = false
                    };
                    db.Usuarios.Add(UsuarioNuevo);
                    db.SaveChanges();

                    TempData["Reporte"] = $"Has sido registrado exitosamente. Bienvenido, {UsuarioNuevo.Nombre}! Te debería llegar un mail en breve. Ya podés comentar nuestros artículos y realizar compras.";
                    TempData["TipoDeReporte"] = "success";
                    Session["Usuario"] = db.Usuarios.FirstOrDefault(u => u.Mail == UsuarioNuevo.Mail);
                    Session["BotonUsuario"] = "borde-azul";
                } else
                {
                    TempData["Reporte"] = "Ya existe un usuario registrado para la dirección de correo ingresada. Puede que ya estés registrado. Probaste ingresar? Por cualquier problema contactate con nosotros desde el mail con el que tuviste dificultades, para resetear tu contraseña.";
                    TempData["TipoDeReporte"] = "danger";
                }
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Admin()
        {
            return View();
        }
    }
}