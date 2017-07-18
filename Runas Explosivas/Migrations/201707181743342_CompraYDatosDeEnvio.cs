namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompraYDatosDeEnvio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Compras",
                c => new
                    {
                        Fecha = c.DateTime(nullable: false),
                        CantidadDeProducto = c.Int(nullable: false),
                        MailEnviado = c.Boolean(nullable: false),
                        OrdenEnViaje = c.Boolean(nullable: false),
                        OrdenRecibida = c.Boolean(nullable: false),
                        NotasAdmin = c.String(),
                        FechaEntregaEstimada = c.DateTime(nullable: false),
                        Comprador_Mail = c.String(maxLength: 100),
                        Producto_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Fecha)
                .ForeignKey("dbo.Usuarios", t => t.Comprador_Mail)
                .ForeignKey("dbo.Productos", t => t.Producto_ID)
                .Index(t => t.Comprador_Mail)
                .Index(t => t.Producto_ID);
            
            CreateTable(
                "dbo.DatosDeEnvios",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosDeEnvios", "Usuario_Mail", "dbo.Usuarios");
            DropForeignKey("dbo.Compras", "Producto_ID", "dbo.Productos");
            DropForeignKey("dbo.Compras", "Comprador_Mail", "dbo.Usuarios");
            DropIndex("dbo.DatosDeEnvios", new[] { "Usuario_Mail" });
            DropIndex("dbo.Compras", new[] { "Producto_ID" });
            DropIndex("dbo.Compras", new[] { "Comprador_Mail" });
            DropTable("dbo.DatosDeEnvios");
            DropTable("dbo.Compras");
        }
    }
}
