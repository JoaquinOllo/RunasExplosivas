using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Runas_Explosivas.Models
{
    public class Usuario
    {
        [Key]
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    }
}