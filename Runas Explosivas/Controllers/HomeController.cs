using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Articulo> articulos = new List<Articulo>();

            ViewBag.Articulos = articulos;

            // transferir artículos del JS a este método.
            // Carga temporal de artículos manual en método Index del controlador Home. Migrar a futuro a base de datos.
            return View();
        }

        public ActionResult Podcast()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Editorial()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            return View();
        }

        public ActionResult ArticuloPrueba()
        {
            return View();
        }

        public ActionResult PruebasVarias()
        {
            return View();
        }

        public ActionResult Mensaje(string nombre, string email, string mensaje, string SendTo)
        {
            SmtpClient clienteSmtp = new SmtpClient();
            clienteSmtp.Host = "smtp.gmail.com";
            clienteSmtp.Port = 587;
            clienteSmtp.Credentials = new NetworkCredential("joaquinollo@gmail.com", "qwe11ny23aZ");
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


    }
}