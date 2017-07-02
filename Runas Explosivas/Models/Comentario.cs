using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class Comentario
    {
        public Usuario Autor { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public Comentario RespuestaA { get; set; }
        public Articulo Articulo { get; set; }
    }
}