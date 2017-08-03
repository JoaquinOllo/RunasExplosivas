using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class ProductoEnCarrito
    {
        [Key]
        public Producto Producto { get; set; }
        [Key]
        public int Cantidad { get; set; }

        public ProductoEnCarrito(Producto inputProducto, int inputCantidad = 1)
        {
            Producto = inputProducto;
            Cantidad = inputCantidad > 0 ? inputCantidad : 1;
        }

        public ProductoEnCarrito(){}
    }
}