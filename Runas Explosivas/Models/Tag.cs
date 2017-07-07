﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace Runas_Explosivas.Models
{
    public class Tags
    {
        public string Nombre { get; set; }
        public string Glyphicon { get; set; }
        public bool Prioridad { get; set; }
        public HtmlString GlyphHTML { get; set; }
        public string Placeholder { get; set; }

        public Tags (string newnombre, string newglyphicon, bool newprioridad)
        {
            Nombre = newnombre;
            Glyphicon = newglyphicon != "" ? $"glyphicon glyphicon-{newglyphicon}" : "";
            Prioridad = newprioridad;
            Placeholder = newglyphicon != "" ? $"<span data-toggle=\"tooltip\" title=\"{newnombre}\" class=\"tag-icon {Glyphicon}\"></span>" : " ";
            GlyphHTML = new HtmlString(Placeholder);
        }
    }
}