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
        public List<Tag> Tags { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public string Link { get; set; }
        public string Imagen { get; set; }
        public string PreviewText { get; set; }
        public List<Comentario> Comentarios { get; set; }
    }
}