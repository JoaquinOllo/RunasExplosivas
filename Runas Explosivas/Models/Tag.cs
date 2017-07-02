using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class Tag
    {
        public string Nombre { get; set; }
        public string Glyphicon { get; set; }
        public bool Prioridad { get; set; }
        public string GlyphHTML { get; set; }

        public Tag (string newnombre, string newglyphicon, bool newprioridad)
        {
            Nombre = newnombre;
            Glyphicon = newglyphicon != "" ? "glyphicon glyphicon-" + newglyphicon : "";
            Prioridad = newprioridad;
            GlyphHTML = newglyphicon != "" ? "/a><span data-toggle=\"tooltip\" title=\"" + newnombre + "\" class=\"tag-icon " + newglyphicon + "\"></span>" : "";
        }
    }
}