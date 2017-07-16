using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    [Table("ComentariosEnArticulos")]
    public class ComentarioEnArticulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public Usuario Autor { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Texto { get; set; }
        public ComentarioEnArticulo RespuestaA { get; set; }
        [Required]
        public Articulo ArticuloComentado { get; set; }

        public int NivelDeDerivacion
        {
            get
            {
                int _NivelDeDerivacion = this.BuscadorRespuestaARecursivo();
                return _NivelDeDerivacion > 4 ? 4 : _NivelDeDerivacion;
            }
        }


        private int BuscadorRespuestaARecursivo(int contador = 0)
        {
            if (RespuestaA != null)
            {
                contador += 1;
                return RespuestaA.BuscadorRespuestaARecursivo(contador);
            } else
            {
                return contador;
            }
        }

    }
}