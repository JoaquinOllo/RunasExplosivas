using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Runas_Explosivas.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string ImagenDePerfil { get; set; }
        public string Descripcion { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsColaborador { get; set; }
    }
}