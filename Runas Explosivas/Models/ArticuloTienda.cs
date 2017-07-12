using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class ArticuloTienda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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