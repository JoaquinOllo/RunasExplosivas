﻿using Runas_Explosivas.Models;
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
        public ActionResult Index()
        {
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
            return View();
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
                ViewBag.Articulo = Articulo;
                return View();
            }
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


    }
}