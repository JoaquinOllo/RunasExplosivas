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
        [EmailAddressAttribute]
        [MaxLength(100, ErrorMessage = "La dirección de correo no puede exceder los 50 caracteres")]
        public string Mail { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "La contraseña no puede exceder los 20 caracteres")]
        public string Password { get; set; }
        [Required]
        [MaxLength(150, ErrorMessage = "El nombre completo no pueden exceder los 150 caracteres")]
        public string Nombre { get; set; }
        [MaxLength(250, ErrorMessage = "La descripción de usuario no puede exceder los 250 caracteres")]
        public string Descripcion { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsColaborador { get; set; }

        public virtual ICollection<Articulo> Articulos { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }

    }
}