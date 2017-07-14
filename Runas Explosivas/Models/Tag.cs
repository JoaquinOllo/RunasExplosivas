using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Runas_Explosivas.Models
{
    public class Tag
    {
        [Key]
        public string Nombre { get; set; }
        public string Glyphicon { get; set; }
        public bool Prioridad { get; set; }
        [NotMapped]
        public HtmlString GlyphHTML
        {
            get
            {
                string Placeholder = Glyphicon != "" ? $"<span data-toggle=\"tooltip\" title=\"{Nombre}\" class=\"tag-icon {Glyphicon}\"></span>" : " ";
                return new HtmlString(Placeholder);
            }
        }

        public Tag (string newnombre, string newglyphicon = "", bool newprioridad = false)
        {
            Nombre = newnombre;
            Glyphicon = newglyphicon != "" ? $"glyphicon glyphicon-{newglyphicon}" : "";
            Prioridad = newprioridad;
        }
        public Tag() { }

        public virtual ICollection<Articulo> Articulos { get; set; }
    }
}