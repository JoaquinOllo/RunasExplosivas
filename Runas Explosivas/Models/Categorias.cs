using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class Categorias
    {
        [Key]
        public string Nombre { get; set; }
        public string Glyphicon { get; set; }
        public string Color { get; set; }
        public bool Prioridad { get; set; }
        public HtmlString GlyphHTML { get; set; }
        public string Descripcion { get; set; }
        public string Placeholder { get; set; }
        public bool StockIlimitado { get; set; }
    }
}