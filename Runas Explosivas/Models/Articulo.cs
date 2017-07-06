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
        public Usuario Autor { get; set; }
        public Tags Tags { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public string Link { get; set; }
        public string Imagen { get; set; }
        public string PreviewText { get; set; }
        public List<Comentario> Comentarios { get; set; }

        public string GetPreviewText(int characters)
        {
            if (characters >= Texto.Length)
            {
                characters = Texto.Length;
            }
            return Texto.Substring(0, characters) + "...";
        }

        public Articulo (int nID, string nTitulo, Usuario nAutor, DateTime nFecha, string nTexto, string nLink, string nImagen, Tags nTags)
        {
            ID = nID;
            Titulo = nTitulo;
            Autor = nAutor;
            Fecha = nFecha;
            Texto = nTexto;
            Link = nLink;
            Imagen = nImagen;
            Tags = nTags;
        }
        /// <summary>
        /// Método para obtener HTML de Glyphicons
        /// </summary>
        /// <param name="Tag">Parámetro Tag de clase Tags</param>
        /// <returns>Devuelve string con el HTML del glyphicon</returns>
        public string AllGlyphHTML()
        {
            return Tags.GlyphHTML;
        }
    }

}