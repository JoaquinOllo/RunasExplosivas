using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class Compra
    {
        [Key]
        public Usuario Comprador { get; set; }
        [Key]
        public Producto Producto { get; set; }
        [Key]
        public DateTime Fecha { get; set; }
        [Required]
        public int CantidadDeProducto { get; set; }
        [Required]
        public bool MailEnviado { get; set; }
        [Required]
        public bool OrdenEnViaje { get; set; }
        [Required]
        public bool OrdenRecibida { get; set; }
        public string NotasAdmin { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }
    }
}