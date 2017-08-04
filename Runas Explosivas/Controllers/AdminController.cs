using Runas_Explosivas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Runas_Explosivas.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

            RunasContext db = new RunasContext();

        /* ACCIONES QUE DEVUELVEN LAS VISTAS */

        public ActionResult Index()
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                List<Usuario> Autores = db.Usuarios.Where(u => u.IsAdmin || u.IsColaborador).ToList();

                ViewBag.Autores = Autores;
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AdmUsuarios()
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                List<Usuario> Autores = db.Usuarios.Where(u => u.IsAdmin || u.IsColaborador).ToList();

                ViewBag.Autores = Autores;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult ArticulosYPodcasts()
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                List<Usuario> Autores = db.Usuarios.Where(u => u.IsAdmin || u.IsColaborador).ToList();

                ViewBag.Autores = Autores;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Editorial()
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                List<Usuario> Autores = db.Usuarios.Where(u => u.IsAdmin || u.IsColaborador).ToList();

                ViewBag.Autores = Autores;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Moderacion()
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                List<Usuario> Autores = db.Usuarios.Where(u => u.IsAdmin || u.IsColaborador).ToList();

                ViewBag.Autores = Autores;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult CrearArticulo(string inputTitulo, string inputAutores, string inputTags, string inputTexto, HttpPostedFileBase inputImagen, string inputLink = null)
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
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
                    }
                    else
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

                string SavePath = HostingEnvironment.ApplicationPhysicalPath + "Content\\Images\\Articulos\\";
                string NombreArchivo = string.Empty;
                if (inputImagen != null && inputImagen.ContentLength > 0)
                {
                    var _NombreArchivo = Path.GetFileName(inputImagen.FileName);
                    var Fullpath = Path.Combine(SavePath, _NombreArchivo);
                    if (System.IO.File.Exists(Fullpath))
                    {
                        int contador = 2;
                        while (System.IO.File.Exists(Fullpath))
                        {
                            _NombreArchivo = _NombreArchivo + contador.ToString();
                            Fullpath = SavePath + _NombreArchivo;
                            contador++;
                        }
                    }
                    inputImagen.SaveAs(Fullpath);
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
                    Imagen = inputImagen != null && inputImagen.ContentLength > 0 ? NombreArchivo : ""
                };

                if (inputLink != "") { NuevoArticulo.Link = inputLink; }

                db.Articulos.Add(NuevoArticulo);
                db.SaveChanges();
                NuevoArticulo = db.Articulos.AsQueryable().OrderByDescending(A => A.Fecha).FirstOrDefault(A => A.Titulo == inputTitulo);
                TempData["Reporte"] = "El artículo fue subido exitosamente!";
                TempData["TipoDeReporte"] = "success";
                return RedirectToAction("Articulo", "Home", new { blogpostID = NuevoArticulo.ID });
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EliminarArticulo (int blogpostID)
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                var ArticuloAEliminar = new Articulo { ID = blogpostID };
                db.Articulos.Attach(ArticuloAEliminar);
                db.Articulos.Remove(ArticuloAEliminar);
                db.SaveChanges();

                TempData["Reporte"] = $"El artículo \"{ArticuloAEliminar.Titulo}\"  fue eliminado exitosamente : (";
                TempData["TipoDeReporte"] = "success";

                return RedirectToAction("Index", "Admin");
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult EditarArticulo(int articuloAModificarID, string inputTitulo, string inputAutores, string inputTags, string inputTexto, HttpPostedFileBase inputImagen, string inputLink = null)
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                var ArticuloAEliminar = db.Articulos.First(A => A.ID == articuloAModificarID);
                DateTime FechaOriginal = ArticuloAEliminar.Fecha;

                db.Articulos.Attach(ArticuloAEliminar);
                db.Articulos.Remove(ArticuloAEliminar);
                db.SaveChanges();

                /* DIVIDIMOS EL STRING RECIBIDO DESDE EL FORMULARIO EN UNA LISTA, 
                 * CONSULTAMOS LA LISTA DE TAGS PARA ENCONTRARLOS, O LOS CREAMOS, 
                 * Y LOS AGREGAMOS A UNA LISTA PROVISORIA */

                List<Tag> ListaTags = new List<Tag>();
                foreach (string Tag in inputTags.Split(' '))
                {
                    if (db.Tags.Any(t => t.Nombre == Tag))
                    {
                        ListaTags.Add(db.Tags.First(t => t.Nombre == Tag));
                    }
                    else
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

                string SavePath = HostingEnvironment.ApplicationPhysicalPath + "Content\\Images\\Articulos\\";
                string NombreArchivo = string.Empty;
                if (inputImagen != null && inputImagen.ContentLength > 0)
                {
                    var _NombreArchivo = Path.GetFileName(inputImagen.FileName);
                    var Fullpath = Path.Combine(SavePath, _NombreArchivo);
                    if (System.IO.File.Exists(Fullpath))
                    {
                        int contador = 2;
                        while (System.IO.File.Exists(Fullpath))
                        {
                            _NombreArchivo = _NombreArchivo + contador.ToString();
                            Fullpath = SavePath + _NombreArchivo;
                            contador++;
                        }
                    }
                    inputImagen.SaveAs(Fullpath);
                    NombreArchivo = _NombreArchivo;
                }

                /* CREACIÓN DEL NUEVO ARTÍCULO, CON LAS LISTAS DE TAGS Y AUTORES CREADAS ANTERIORMENTE
                 * Y LOS PARÁMETROS PASADOS A LA ACCIÓN */

                Articulo NuevoArticulo = new Articulo()
                {
                    Titulo = inputTitulo,
                    Texto = inputTexto,
                    Fecha = FechaOriginal,
                    Autores = ListaAutores,
                    Tags = ListaTags,
                    Imagen = inputImagen != null && inputImagen.ContentLength > 0 ? NombreArchivo : ""
                };

                if (inputLink != "") { NuevoArticulo.Link = inputLink; }

                db.Articulos.Add(NuevoArticulo);
                db.SaveChanges();
                NuevoArticulo = db.Articulos.AsQueryable().OrderByDescending(A => A.Fecha).FirstOrDefault(A => A.Titulo == inputTitulo);
                TempData["Reporte"] = "El artículo fue editado exitosamente!";
                TempData["TipoDeReporte"] = "success";

                return RedirectToAction("Articulo", "Home", new { blogpostID = NuevoArticulo.ID });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult CrearProducto(string inputTitulo, string inputAutores, string inputCategorias, string inputTexto, HttpPostedFileBase inputImagen, HttpPostedFileBase inputLink, string inputPrecio, string inputStock)
        {
            if (((Usuario)Session["Usuario"]) != null && ((Usuario)Session["Usuario"]).IsAdmin)
            {
                /* DIVIDIMOS EL STRING RECIBIDO DESDE EL FORMULARIO EN UNA LISTA, 
                 * CONSULTAMOS LA LISTA DE CATEGORÍAS PARA ENCONTRARLAS, O LAS CREAMOS, 
                 * Y LAS AGREGAMOS A UNA LISTA PROVISORIA */

                List<Categoria> ListaCategorias = new List<Categoria>();
                foreach (string Categoria in inputCategorias.Split(' '))
                {
                    if (db.Categorias.Any(c => c.Nombre == Categoria))
                    {
                        ListaCategorias.Add(db.Categorias.First(c => c.Nombre == Categoria));
                    }
                    else
                    {
                        Categoria NuevaCategoria = new Models.Categoria(Categoria);
                        db.Categorias.Add(NuevaCategoria);
                        db.SaveChanges();
                        ListaCategorias.Add(db.Categorias.First(c => c.Nombre == Categoria));
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

                string SavePath = HostingEnvironment.ApplicationPhysicalPath + "Content\\Images\\Articulos\\";
                string NombreArchivo = string.Empty;
                if (inputImagen != null && inputImagen.ContentLength > 0)
                {
                    var _NombreArchivo = Path.GetFileName(inputImagen.FileName);
                    var Fullpath = Path.Combine(SavePath, _NombreArchivo);
                    if (System.IO.File.Exists(Fullpath))
                    {
                        int contador = 2;
                        while (System.IO.File.Exists(Fullpath))
                        {
                            _NombreArchivo = _NombreArchivo + contador.ToString();
                            Fullpath = SavePath + _NombreArchivo;
                            contador++;
                        }
                    }
                    inputImagen.SaveAs(Fullpath);
                    NombreArchivo = _NombreArchivo;
                }

                /* LINK PARA SUBIR PDF DEL PRODUCTO */



                /* CREACIÓN DEL NUEVO ARTÍCULO, CON LAS LISTAS DE TAGS Y AUTORES CREADAS ANTERIORMENTE
                 * Y LOS PARÁMETROS PASADOS A LA ACCIÓN */

                Producto NuevoProducto = new Producto()
                {
                    Titulo = inputTitulo,
                    Texto = inputTexto,
                    Fecha = DateTime.Now,
                    Autores = ListaAutores,
                    Categorias = ListaCategorias,
                    Imagen = inputImagen != null && inputImagen.ContentLength > 0 ? NombreArchivo : "",
                    Precio = ListaCategorias.Select(C => C.Nombre).Contains("gratis") ? 0 : float.Parse(inputPrecio),
                    Stock = ListaCategorias.Select(C => C.Nombre).Contains("digital") ? 0 : int.Parse(inputStock)
                };

                db.Productos.Add(NuevoProducto);
                db.SaveChanges();
                NuevoProducto = db.Productos.AsQueryable().OrderByDescending(A => A.Fecha).FirstOrDefault(A => A.Titulo == inputTitulo);
                TempData["Reporte"] = "El producto fue subido exitosamente!";
                TempData["TipoDeReporte"] = "success";
                return RedirectToAction("Producto", "Editorial", new { prodID = NuevoProducto.ID });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        /* ACCIÓN QUE DEVUELVE ARTÍCULOS INDIVIDUALES POR AJAX */

        public JsonResult ArticuloIndividual(int artID)
        {
            Articulo ResultadoBusqueda = db.Articulos.First(A => A.ID == artID);

            var resultadoFinal = new
            {
                ID = ResultadoBusqueda.ID,
                Titulo = ResultadoBusqueda.Titulo,
                Link = ResultadoBusqueda.Link,
                Texto = ResultadoBusqueda.Texto,
                Imagen = ResultadoBusqueda.Imagen,
                Autores = string.Join(" ", ResultadoBusqueda.Autores.Select(A => A.Mail)),
                Tags = string.Join(" ", ResultadoBusqueda.Tags.Select(A => A.Nombre))
            };

            return Json(resultadoFinal, JsonRequestBehavior.AllowGet);
        }
    }
}