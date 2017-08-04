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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public Usuario Comprador { get; set; }
        [Required]
        public virtual ICollection<ProductoEnCarro> Productos { get; set; }
        public DatosDeEnvio DatosDeEnvio { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public bool MailEnviado { get; set; }
        [Required]
        public bool CompraAbonada { get; set; }
        public string FormaDePago { get; set; }
        public bool OrdenEnViaje { get; set; }
        public bool OrdenRecibida { get; set; }
        public string NotasAdmin { get; set; }
        public DateTime FechaEntregaEstimada { get; set; }

        public float PrecioTotal
        {
            get
            {
                return Productos.Sum(C => C.Cantidad * C.Producto.Precio);
            }
        }
        public Compra() { }
    }
}