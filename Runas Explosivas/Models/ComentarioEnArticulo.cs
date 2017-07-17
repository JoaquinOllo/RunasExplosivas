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

        /// <summary>
        /// Indica, si lo hay, el comentario al que responde esta instancia.
        /// </summary>
        public ComentarioEnArticulo RespuestaA { get; set; }
        [Required]
        public Articulo ArticuloComentado { get; set; }

        /// <summary>
        /// Propiedad que indica el estilo CSS del comentario en la vista, según a cuántos comntarios esté respondiendo
        /// </summary>
        public int NivelDeDerivacion
        {
            get
            {
                if (RespuestaA != null)
                {
                    return RespuestaA.NivelDeDerivacion + 1 < 4 ? RespuestaA.NivelDeDerivacion + 1 : 3;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}