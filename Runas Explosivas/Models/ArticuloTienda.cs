using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class ArticuloTienda
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public List<Usuario> Autor { get; set; }
        public List<Categorias> Categorias { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public float Precio { get; set; }
        public int Stock { get; set; }
    }
}