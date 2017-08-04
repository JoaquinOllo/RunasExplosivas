using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class ProductoEnCarro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public Producto Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }

        public ProductoEnCarro(Producto inputProducto, int inputCantidad = 1)
        {
            Producto = inputProducto;
            Cantidad = inputCantidad > 0 ? inputCantidad : 1;
        }

        public ProductoEnCarro() { }
    }
}