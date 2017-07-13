using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("Categorias")]
    public class Categoria : Tag
    {
        public Categoria(string newnombre, string newglyphicon, bool newprioridad, string newcolor, string newdescripcion, bool newstockilimitado) : base(newnombre, newglyphicon, newprioridad)
        {
            Color = newcolor;
            Descripcion = newdescripcion;
            HasStockIlimitado = newstockilimitado;
        }

        public string Color { get; set; }
        public string Descripcion { get; set; }
        public bool HasStockIlimitado { get; set; }
    }
}