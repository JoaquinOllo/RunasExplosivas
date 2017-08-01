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
        RunasContext db = new RunasContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DatosDeEnvio()
        {
            if (Session["Usuario"] != null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Usuario
        public ActionResult Login(string inputEmail, string inputPassword)
        {
            Usuario UsuarioConectado = db.Usuarios.SingleOrDefault(us => us.Mail == inputEmail && us.Password == inputPassword);
            DatosDeEnvio UsuarioConectadoDatosEnvio = db.TablaDatosDeEnvio.FirstOrDefault(DE => DE.Usuario.Mail == inputEmail);

            if (UsuarioConectado == default(Usuario))
            {
                TempData["Reporte"] = "La dirección ingresada o la contraseña son incorrectas. Por favor, intentá nuevamente";
                TempData["TipoDeReporte"] = "danger";
            }
            else
            {
                Session["Usuario"] = UsuarioConectado;
                if (UsuarioConectadoDatosEnvio != default(DatosDeEnvio))
                {
                    Session["DatosDeEnvio"] = UsuarioConectadoDatosEnvio;
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegistroUsuarioNuevo(string Mail, string Password, string NombreCompleto, string Descripcion)
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
            }
            else
            {
                TempData["Reporte"] = "Ya existe un usuario registrado para la dirección de correo ingresada. Puede que ya estés registrado. Probaste ingresar? Por cualquier problema contactate con nosotros desde el mail con el que tuviste dificultades, para resetear tu contraseña.";
                TempData["TipoDeReporte"] = "danger";
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegistroDatosDeEnvio (string inputCiudad, string inputProvincia, 
            string inputPais, string inputZipCode, string inputCalle, string inputNumero, 
            int inputTel, string inputDatosAdicionales, string inputDepartamento = null)
        {
            if (Session["Usuario"] != null)
            {
                string UsuarioMail = ((Usuario)Session["Usuario"]).Mail;
                Usuario Usuario = db.Usuarios.FirstOrDefault( U => U.Mail == UsuarioMail);

                DatosDeEnvio DatosDeEnvioUC = new DatosDeEnvio()
                {
                    Usuario = Usuario,
                    Ciudad = inputCiudad,
                    Provincia = inputProvincia,
                    Pais = inputPais,
                    ZipCode = inputZipCode,
                    Calle = inputCalle,
                    Numero = inputNumero,
                    Departamento = inputDepartamento,
                    Telefono = inputTel,
                    DatosAdicionales = inputDatosAdicionales
                };

                db.TablaDatosDeEnvio.Add(DatosDeEnvioUC);
                db.SaveChanges();

                TempData["Reporte"] = "Tus datos de envío fueron guardados correctamente. Ya podés hacer tu primera compra!";
                TempData["TipoDeReporte"] = "success";
                Session["DatosDeEnvio"] = db.TablaDatosDeEnvio.Where(DE => DE.Usuario.Mail == UsuarioMail);
                return RedirectToAction("Index", "Home");
            }
            else
            {
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