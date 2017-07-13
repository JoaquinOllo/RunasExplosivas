using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("Resenhas")]
    public class Resenha : Comentario
    {
        public float Puntuacion { get; set; }
        public Producto ProductoResenhado { get; set; }
    }
}