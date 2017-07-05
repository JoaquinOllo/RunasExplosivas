using System;
using System.Collections.Generic;

namespace Runas_Explosivas.Models
{
    public class Usuario
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }

        public static implicit operator List<object>(Usuario v)
        {
            throw new NotImplementedException();
        }
    }
}