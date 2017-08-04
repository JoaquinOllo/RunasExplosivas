namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articulos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 100),
                        Fecha = c.DateTime(nullable: false),
                        Link = c.String(),
                        Texto = c.String(nullable: false),
                        Imagen = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Mail = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 20),
                        Nombre = c.String(nullable: false, maxLength: 150),
                        Descripcion = c.String(maxLength: 250),
                        IsAdmin = c.Boolean(nullable: false),
                        IsColaborador = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Mail);
            
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        MailEnviado = c.Boolean(nullable: false),
                        CompraAbonada = c.Boolean(nullable: false),
                        FormaDePago = c.String(),
                        OrdenEnViaje = c.Boolean(nullable: false),
                        OrdenRecibida = c.Boolean(nullable: false),
                        NotasAdmin = c.String(),
                        FechaEntregaEstimada = c.DateTime(nullable: false),
                        Comprador_Mail = c.String(nullable: false, maxLength: 100),
                        DatosDeEnvio_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuarios", t => t.Comprador_Mail, cascadeDelete: true)
                .ForeignKey("dbo.TablaDatosDeEnvio", t => t.DatosDeEnvio_ID)
                .Index(t => t.Comprador_Mail)
                .Index(t => t.DatosDeEnvio_ID);
            
            CreateTable(
                "dbo.TablaDatosDeEnvio",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ciudad = c.String(nullable: false, maxLength: 40),
                        Provincia = c.String(nullable: false, maxLength: 40),
                        Pais = c.String(nullable: false, maxLength: 40),
                        ZipCode = c.String(nullable: false, maxLength: 10),
                        Calle = c.String(nullable: false, maxLength: 50),
                        Numero = c.String(nullable: false),
                        Departamento = c.String(maxLength: 10),
                        Telefono = c.Int(nullable: false),
                        DatosAdicionales = c.String(),
                        Usuario_Mail = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Mail, cascadeDelete: true)
                .Index(t => t.Usuario_Mail);
            
            CreateTable(
                "dbo.ProductoEnCarroes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        Producto_ID = c.Int(nullable: false),
                        Compra_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Productos", t => t.Producto_ID, cascadeDelete: true)
                .ForeignKey("dbo.Compras", t => t.Compra_ID)
                .Index(t => t.Producto_ID)
                .Index(t => t.Compra_ID);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false, maxLength: 50),
                        Fecha = c.DateTime(nullable: false),
                        Link = c.String(),
                        Texto = c.String(nullable: false),
                        Imagen = c.String(nullable: false),
                        Stock = c.Int(nullable: false),
                        Precio = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Prioridad = c.Boolean(nullable: false),
                        Color = c.String(),
                        HasStockIlimitado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Nombre);
            
            CreateTable(
                "dbo.Resenhas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Texto = c.String(nullable: false),
                        Puntuacion = c.Single(nullable: false),
                        Autor_Mail = c.String(nullable: false, maxLength: 100),
                        ProductoResenhado_ID = c.Int(nullable: false),
                        RespuestaA_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuarios", t => t.Autor_Mail, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoResenhado_ID, cascadeDelete: true)
                .ForeignKey("dbo.Resenhas", t => t.RespuestaA_ID)
                .Index(t => t.Autor_Mail)
                .Index(t => t.ProductoResenhado_ID)
                .Index(t => t.RespuestaA_ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Glyphicon = c.String(maxLength: 100),
                        Prioridad = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Nombre);
            
            CreateTable(
                "dbo.ComentariosEnArticulos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Texto = c.String(nullable: false),
                        ArticuloComentado_ID = c.Int(nullable: false),
                        Autor_Mail = c.String(nullable: false, maxLength: 100),
                        RespuestaA_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Articulos", t => t.ArticuloComentado_ID, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Autor_Mail, cascadeDelete: true)
                .ForeignKey("dbo.ComentariosEnArticulos", t => t.RespuestaA_ID)
                .Index(t => t.ArticuloComentado_ID)
                .Index(t => t.Autor_Mail)
                .Index(t => t.RespuestaA_ID);
            
            CreateTable(
                "dbo.UsuarioArticuloes",
                c => new
                    {
                        Usuario_Mail = c.String(nullable: false, maxLength: 100),
                        Articulo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_Mail, t.Articulo_ID })
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Mail, cascadeDelete: true)
                .ForeignKey("dbo.Articulos", t => t.Articulo_ID, cascadeDelete: true)
                .Index(t => t.Usuario_Mail)
                .Index(t => t.Articulo_ID);
            
            CreateTable(
                "dbo.ProductoUsuarios",
                c => new
                    {
                        Producto_ID = c.Int(nullable: false),
                        Usuario_Mail = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => new { t.Producto_ID, t.Usuario_Mail })
                .ForeignKey("dbo.Productos", t => t.Producto_ID, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Mail, cascadeDelete: true)
                .Index(t => t.Producto_ID)
                .Index(t => t.Usuario_Mail);
            
            CreateTable(
                "dbo.CategoriaProductoes",
                c => new
                    {
                        Categoria_Nombre = c.String(nullable: false, maxLength: 50),
                        Producto_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Categoria_Nombre, t.Producto_ID })
                .ForeignKey("dbo.Categorias", t => t.Categoria_Nombre, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.Producto_ID, cascadeDelete: true)
                .Index(t => t.Categoria_Nombre)
                .Index(t => t.Producto_ID);
            
            CreateTable(
                "dbo.TagArticuloes",
                c => new
                    {
                        Tag_Nombre = c.String(nullable: false, maxLength: 50),
                        Articulo_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Nombre, t.Articulo_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_Nombre, cascadeDelete: true)
                .ForeignKey("dbo.Articulos", t => t.Articulo_ID, cascadeDelete: true)
                .Index(t => t.Tag_Nombre)
                .Index(t => t.Articulo_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComentariosEnArticulos", "RespuestaA_ID", "dbo.ComentariosEnArticulos");
            DropForeignKey("dbo.ComentariosEnArticulos", "Autor_Mail", "dbo.Usuarios");
            DropForeignKey("dbo.ComentariosEnArticulos", "ArticuloComentado_ID", "dbo.Articulos");
            DropForeignKey("dbo.TagArticuloes", "Articulo_ID", "dbo.Articulos");
            DropForeignKey("dbo.TagArticuloes", "Tag_Nombre", "dbo.Tags");
            DropForeignKey("dbo.Resenhas", "RespuestaA_ID", "dbo.Resenhas");
            DropForeignKey("dbo.Resenhas", "ProductoResenhado_ID", "dbo.Productos");
            DropForeignKey("dbo.Resenhas", "Autor_Mail", "dbo.Usuarios");
            DropForeignKey("dbo.ProductoEnCarroes", "Compra_ID", "dbo.Compras");
            DropForeignKey("dbo.ProductoEnCarroes", "Producto_ID", "dbo.Productos");
            DropForeignKey("dbo.CategoriaProductoes", "Producto_ID", "dbo.Productos");
            DropForeignKey("dbo.CategoriaProductoes", "Categoria_Nombre", "dbo.Categorias");
            DropForeignKey("dbo.ProductoUsuarios", "Usuario_Mail", "dbo.Usuarios");
            DropForeignKey("dbo.ProductoUsuarios", "Producto_ID", "dbo.Productos");
            DropForeignKey("dbo.Compras", "DatosDeEnvio_ID", "dbo.TablaDatosDeEnvio");
            DropForeignKey("dbo.TablaDatosDeEnvio", "Usuario_Mail", "dbo.Usuarios");
            DropForeignKey("dbo.Compras", "Comprador_Mail", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioArticuloes", "Articulo_ID", "dbo.Articulos");
            DropForeignKey("dbo.UsuarioArticuloes", "Usuario_Mail", "dbo.Usuarios");
            DropIndex("dbo.TagArticuloes", new[] { "Articulo_ID" });
            DropIndex("dbo.TagArticuloes", new[] { "Tag_Nombre" });
            DropIndex("dbo.CategoriaProductoes", new[] { "Producto_ID" });
            DropIndex("dbo.CategoriaProductoes", new[] { "Categoria_Nombre" });
            DropIndex("dbo.ProductoUsuarios", new[] { "Usuario_Mail" });
            DropIndex("dbo.ProductoUsuarios", new[] { "Producto_ID" });
            DropIndex("dbo.UsuarioArticuloes", new[] { "Articulo_ID" });
            DropIndex("dbo.UsuarioArticuloes", new[] { "Usuario_Mail" });
            DropIndex("dbo.ComentariosEnArticulos", new[] { "RespuestaA_ID" });
            DropIndex("dbo.ComentariosEnArticulos", new[] { "Autor_Mail" });
            DropIndex("dbo.ComentariosEnArticulos", new[] { "ArticuloComentado_ID" });
            DropIndex("dbo.Resenhas", new[] { "RespuestaA_ID" });
            DropIndex("dbo.Resenhas", new[] { "ProductoResenhado_ID" });
            DropIndex("dbo.Resenhas", new[] { "Autor_Mail" });
            DropIndex("dbo.ProductoEnCarroes", new[] { "Compra_ID" });
            DropIndex("dbo.ProductoEnCarroes", new[] { "Producto_ID" });
            DropIndex("dbo.TablaDatosDeEnvio", new[] { "Usuario_Mail" });
            DropIndex("dbo.Compras", new[] { "DatosDeEnvio_ID" });
            DropIndex("dbo.Compras", new[] { "Comprador_Mail" });
            DropTable("dbo.TagArticuloes");
            DropTable("dbo.CategoriaProductoes");
            DropTable("dbo.ProductoUsuarios");
            DropTable("dbo.UsuarioArticuloes");
            DropTable("dbo.ComentariosEnArticulos");
            DropTable("dbo.Tags");
            DropTable("dbo.Resenhas");
            DropTable("dbo.Categorias");
            DropTable("dbo.Productos");
            DropTable("dbo.ProductoEnCarroes");
            DropTable("dbo.TablaDatosDeEnvio");
            DropTable("dbo.Compras");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Articulos");
        }
    }
}
