using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        [Key]
        [MaxLength(50, ErrorMessage = "El nombre de la categoría no pueden exceder los 50 caracteres")]
        public string Nombre { get; set; }
        [MaxLength(50, ErrorMessage = "El glyphicon no pueden exceder los 100 caracteres")]
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

        public Categoria(string newnombre, string newglyphicon, bool newprioridad, string newcolor, string newdescripcion, bool newstockilimitado)
        {
            Nombre = newnombre;
            Glyphicon = newglyphicon;
            Prioridad = newprioridad;
            Color = newcolor;
            Descripcion = newdescripcion;
            HasStockIlimitado = newstockilimitado;
        }

        public string Color { get; set; }
        public string Descripcion { get; set; }
        public bool HasStockIlimitado { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}