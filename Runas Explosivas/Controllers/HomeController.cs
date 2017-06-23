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

        /// <summary>
        /// Método para enviar formulario de consulta desde la página de contacto.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="email"></param>
        /// <param name="mensaje"></param>
        /// <param name="sendTo">Indica a quién enviar el email</param>
        /// <returns>Devuelve al index</returns>

        public ActionResult Mensaje(string nombre, string email, string mensaje, string sendTo)
        {
            SmtpClient clienteSmtp = new SmtpClient();
            clienteSmtp.Host = "smtp.gmail.com";
            clienteSmtp.Port = 587;
            clienteSmtp.Credentials = new NetworkCredential("runasexplosivas@gmail.com", "runex6d6287");
            clienteSmtp.EnableSsl = true;


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("runasexplosivas@gmail.com", "Mensaje dejado en página");
            mail.To.Add("runasexplosivas@gmail.com");
            mail.Subject = "Te contactaron de la página!";
            mail.Body = nombre + " (" + email + ") te contactó desde la aplicación, y te dejó el siguiente mensaje:" + mensaje;
            clienteSmtp.Send(mail);

            //mail para el usuario
            MailMessage mensajeParaUsuario = new MailMessage();
            mensajeParaUsuario.From = new MailAddress("runasexplosivas@gmail.com", "Runas Explosivas");
            mensajeParaUsuario.To.Add(email);
            mensajeParaUsuario.Subject = "Gracias por contactarte con el Blog!";
            mensajeParaUsuario.Body = "Gracias por contactarte con el staff de Runas Explosivas! Tu mail fue enviado, y será respondido a la brevedad.";

            clienteSmtp.Send(mensajeParaUsuario);
            return View("Index");
        }
    }
}