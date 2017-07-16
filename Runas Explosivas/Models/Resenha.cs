using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("Resenhas")]
    public class Resenha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public Usuario Autor { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Texto { get; set; }
        public Resenha RespuestaA { get; set; }
        [Range(1, 10)]
        public float Puntuacion { get; set; }
        [Required]
        public Producto ProductoResenhado { get; set; }
    }
}