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

        /// <summary>
        /// Propiedad que devuelve la fecha del comentario primero al que responde este comentario, para facilitar el ordenamiento.
        /// </summary>
        public DateTime GetRootDate
        {
            get
            {
                if (RespuestaA != null)
                {
                    return RespuestaA.GetRootDate;
                }
                else
                {
                    return Fecha;
                }
            }
        }

        /// <summary>
        /// Cuando un Comentario tiene nivel de derivación mayor que 0 (responde a otro comentario), se le cambia la fecha
        /// para poder ordenarlo apropiadamente al mostrarlo en la página. Cuando esto sucede, la fecha original es alojada
        /// en esta propiedad.
        /// </summary>
        [NotMapped]
        public DateTime FechaOriginal { get; set; }


        /// <summary>
        /// Propiedad que devuelve Fecha si el nivel de derivación del comentario es 0, o FechaOriginal si el comentario
        /// responde a otro comentario.
        /// </summary>
        public DateTime GetTrueFecha
        {
            get
            {
                return NivelDeDerivacion > 0 ? FechaOriginal : Fecha;
            }
        }
    }
}