using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class CarritoDeCompras
    {
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }

        public CarritoDeCompras(Producto inputProducto, int inputCantidad = 1)
        {
            Producto = inputProducto;
            Cantidad = inputCantidad > 0 ? inputCantidad : 1;
        }
    }
}