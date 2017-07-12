using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class Resenha
    {
        public int ID { get; set; }
        public Usuario Autor { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public float Puntuacion { get; set; }
        public Resenha RespuestaA { get; set; }
        public ArticuloTienda Producto { get; set; }
    }
}