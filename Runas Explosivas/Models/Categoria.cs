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
        public bool Prioridad { get; set; }

        public Categoria(string newnombre, bool newprioridad = false, string newcolor = null)
        {
            Nombre = newnombre;
            Prioridad = newprioridad;
            Color = newcolor;
            HasStockIlimitado = newnombre == "digital" ? true : false;
        }
        public Categoria() { }

        public string Color { get; set; }
        public bool HasStockIlimitado { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}