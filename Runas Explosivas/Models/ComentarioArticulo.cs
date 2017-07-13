using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("ComentariosEnArticulos")]
    public class ComentarioEnArticulo : Comentario
    {
        public Articulo ArticuloComentado { get; set; }
    }
}