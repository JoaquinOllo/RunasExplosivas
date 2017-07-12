﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class Resenha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Usuario Autor { get; set; }
        public DateTime Fecha { get; set; }
        public string Texto { get; set; }
        public float Puntuacion { get; set; }
        public Resenha RespuestaA { get; set; }
        public ArticuloTienda Producto { get; set; }
    }
}