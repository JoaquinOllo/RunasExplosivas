using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class UsuarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario
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
                }
                return RedirectToAction("Index", "Home");
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
                }
                else
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
    }
}