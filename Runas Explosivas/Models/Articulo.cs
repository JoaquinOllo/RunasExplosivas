using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{

    /// <summary>
    /// Representa a la entidad Artículo del blog
    /// </summary>
    public class Articulo
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public List<Usuario> Autor { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public string Link { get; set; }
        public string Imagen { get; set; }
        public string PreviewText { get; set; }
        public List<Comentario> Comentarios { get; set; }

        public string GetPreviewText(int characters)
        {
            return Texto.Substring(0, characters) + "...";
        }

        public Articulo (int nID, string nTitulo, List<Usuario> nAutor, DateTime nFecha, string nTexto, string nLink, string nImagen)
        {
            ID = nID;
            Titulo = nTitulo;
            Autor = nAutor;
            Fecha = nFecha;
            Texto = nTexto;
            Link = nLink;
            Imagen = nImagen;

        }
    }
}