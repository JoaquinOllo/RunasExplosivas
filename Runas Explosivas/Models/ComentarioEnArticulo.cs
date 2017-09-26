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
        [Required]
        public int Derivacion { get; set; }

        /// <summary>
        /// Propiedad que indica el estilo CSS del comentario en la vista, según a cuántos comntarios esté respondiendo
        /// </summary>
        public int CalculoDerivacion
        {
            get
            {
                if (RespuestaA != null)
                {
                    return RespuestaA.Derivacion + 1 < 4 ? RespuestaA.Derivacion + 1 : 3;
                }
                else
                {
                    return 0;
                }
            }
        }

        public ComentarioEnArticulo(Usuario inputAutor, DateTime inputFecha, string inputTexto, 
            Articulo inputArticuloComentado, ComentarioEnArticulo inputRespuestaA = null)
        {
            Autor = inputAutor;
            Fecha = inputFecha;
            Texto = inputTexto;

            RespuestaA = inputRespuestaA;

            ArticuloComentado = inputArticuloComentado;
            Derivacion = CalculoDerivacion;
        }

        public ComentarioEnArticulo () { }
    }
}