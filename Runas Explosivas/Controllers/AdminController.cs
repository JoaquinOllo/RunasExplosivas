using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                using (RunasContext db = new Models.RunasContext())
                {
                    List<Usuario> Autores = db.Usuarios.Where(u => u.IsAdmin || u.IsColaborador).ToList();
                    //List<Tag> Tags = db.Tags.ToList();
                    //List<Categoria> Categorias = db.Categorias.ToList();

                    ViewBag.Autores = Autores;
                    //ViewBag.Tags = Tags;
                    //ViewBag.Categorias = Categorias;
                }
                    return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }

        public ActionResult CrearArticulo(string inputTitulo, string inputAutores, string inputTags, string inputTexto, HttpPostedFileBase inputFile, string inputLink = null)
        {
            using (RunasContext db = new Models.RunasContext())
            {
                /* DIVIDIMOS EL STRING RECIBIDO DESDE EL FORMULARIO EN UNA LISTA, 
                 * CONSULTAMOS LA LISTA DE TAGS PARA ENCONTRARLOS, O LOS CREAMOS, 
                 * Y LOS AGREGAMOS A UNA LISTA PROVISORIA */

                List<Tag> ListaTags = new List<Tag>();
                foreach (string Tag in inputTags.Split(' '))
                {
                    if (db.Tags.Any(t => t.Nombre == Tag))
                    {
                        ListaTags.Add(db.Tags.First(t => t.Nombre == Tag));
                    } else
                    {
                        Tag NuevoTag = new Models.Tag(Tag);
                        db.Tags.Add(NuevoTag);
                        db.SaveChanges();
                        ListaTags.Add(db.Tags.First(t => t.Nombre == Tag));
                    }
                }

                /* CONSULTAMOS LA BASE DE DATOS POR LOS AUTORES POSIBLES, Y SEPARAMOS EL STRING RECIBIDO 
                 * DESDE EL FORMULARIO EN UNA LISTA DE STRING. ITERAMOS ESA LISTA, BUSCANDO EN LOS 
                 * AUTORES POSIBLES CADA STRING, Y SI ESTÁ LO AGREGAMOS A UNA LISTA PROVISORIA DE AUTORES */

                IQueryable<Usuario> AutoresPosibles = db.Usuarios.Where(u => u.IsAdmin || u.IsColaborador);
                List<Usuario> ListaAutores = new List<Usuario>();
                foreach (string MailAutor in inputAutores.Split(' '))
                {
                    if (AutoresPosibles.Any(au => au.Mail == MailAutor))
                    {
                        ListaAutores.Add(AutoresPosibles.FirstOrDefault(au => au.Mail == MailAutor));
                    }
                }

                /* LA DIRECCIÓN EN LA QUE SE GUARDARÁ LA IMAGEN ES UNA COMBINACIÓN DE UNA SAVEPATH PREDEFINIDA
                 * MÁS EL NOMBRE DEL ARCHIVO. PRMERO REVISAMOS SI EFECTIVAMENTE SE ENVIÓ UN ARCHIVO, LUEGO
                 * SI YA HAY UN ARCHIVO GUARDADO EN DICHA DIRECCIÓN, Y SI DICHO ARCHIVO YA EXISTE, CAMBIAMOS
                 * EL NOMBRE AGREGÁNDOLE UN NÚMERO HASTA QUE NO HAYA OTRO ARCHIVO CON EL MISMO NOMBRE. 
                 * ENTONCES LO GUARDAMOS */

                string SavePath = "~/Content/Images/Articulos/";
                string NombreArchivo = string.Empty;
                if (inputFile != null && inputFile.ContentLength > 0)
                {
                    string _NombreArchivo = Path.GetFileName(inputFile.FileName);
                    string Fullpath = Path.Combine(SavePath, _NombreArchivo);
                    if (System.IO.File.Exists(Fullpath))
                    {
                        int contador = 2;
                        while (System.IO.File.Exists(Fullpath))
                        {
                            _NombreArchivo = contador.ToString() + _NombreArchivo;
                            Fullpath = SavePath + _NombreArchivo;
                            contador++;
                        }
                    }
                    inputFile.SaveAs(Fullpath);
                    NombreArchivo = _NombreArchivo;
                }

                /* CREACIÓN DEL NUEVO ARTÍCULO, CON LAS LISTAS DE TAGS Y AUTORES CREADAS ANTERIORMENTE
                 * Y LOS PARÁMETROS PASADOS A LA ACCIÓN */

                Articulo NuevoArticulo = new Articulo()
                {
                    Titulo = inputTitulo,
                    Texto = inputTexto,
                    Fecha = DateTime.Now,
                    Autores = ListaAutores,
                    Tags = ListaTags,
                    Imagen = inputFile != null && inputFile.ContentLength > 0 ? NombreArchivo : ""
                };

                if (inputLink != "") { NuevoArticulo.Link = inputLink; }

                db.Articulos.Add(NuevoArticulo);
                db.SaveChanges();
                NuevoArticulo = db.Articulos.AsQueryable().OrderByDescending(A => A.Fecha).FirstOrDefault(A => A.Titulo == inputTitulo);
                return RedirectToAction("Articulo", "Home", new { blogpostID = NuevoArticulo.ID });
            }
        }
    }
}