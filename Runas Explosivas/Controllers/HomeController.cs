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
        RunasContext db = new RunasContext();

        public ActionResult Index()
        {
            List<Articulo> Articulos = db.Articulos.Include("Tags").AsQueryable().OrderByDescending(u => u.Fecha).Take(20).ToList();
            ViewBag.Articulos = Articulos;
            return View();
        }

        public ActionResult Podcast()
        {
            List<Articulo> ArticulosFiltradosPodcast = db.Articulos.Include("Tags").AsQueryable().Where(a => a.Tags.Any(t => t.Nombre == "podcast")).OrderByDescending(a => a.Fecha).Take(20).ToList();
            ViewBag.Articulos = ArticulosFiltradosPodcast;
            return View();
        }

        public ActionResult Blog()
        {
            List<Articulo> ArticulosFiltradosBlog = db.Articulos.Include("Tags").AsQueryable().Where(a => a.Tags.Any(t => t.Nombre == "blog")).OrderByDescending(a => a.Fecha).Take(20).ToList();
            ViewBag.Articulos = ArticulosFiltradosBlog;
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult Articulo(int blogpostID)
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

        public JsonResult BuscarArticulo (string TipoDeBusqueda, string ValorABuscar)
        {
            List<Articulo> ResultadosBusqueda = new List<Models.Articulo>() { };
            if (TipoDeBusqueda == "Tag")
            {
                ResultadosBusqueda = db.Articulos.Where(A => A.Tags.Any(t => t.Nombre == ValorABuscar)).ToList();
            } else if (TipoDeBusqueda == "Autor")
            {
                ResultadosBusqueda = db.Articulos.Where(A => A.Autores.Any(au => au.Nombre.Contains(ValorABuscar))).ToList();
            } else
            {
                ResultadosBusqueda = db.Articulos.Where(A => A.Titulo.Contains(ValorABuscar)).ToList();
            }
            var resultadoFinal = ResultadosBusqueda.Select(a => new { ID = a.ID, Titulo = a.Titulo }).Take(20);

            return Json(resultadoFinal, JsonRequestBehavior.AllowGet);
        }
    }
}