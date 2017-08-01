using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class DatosDeEnvio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public Usuario Usuario { get; set; }
        [Required]
        [MaxLength(40)]
        public string Ciudad { get; set; }
        [Required]
        [MaxLength(40)]
        public string Provincia { get; set; }
        [Required]
        [MaxLength(40)]
        public string Pais { get; set; }
        [Required]
        [MaxLength(10)]
        public string ZipCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string Calle { get; set; }
        [Required]
        public string Numero { get; set; }
        [MaxLength(10)]
        public string Departamento { get; set; }
        [Required]
        public int Telefono { get; set; }
        public string DatosAdicionales { get; set; }

        public DatosDeEnvio() { }
    }
}