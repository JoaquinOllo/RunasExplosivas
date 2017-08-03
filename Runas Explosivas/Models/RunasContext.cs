using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Runas_Explosivas.Models
{
    public class RunasContext : DbContext
    {
        public RunasContext() : base ("name=RunasContext") { }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ComentarioEnArticulo> ComentariosEnArticulos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Resenha> Resenhas { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DatosDeEnvio> TablaDatosDeEnvio { get; set; }
        public DbSet<ProductoEnCarrito> ProductosEnCarrito { get; set; }
    }
}