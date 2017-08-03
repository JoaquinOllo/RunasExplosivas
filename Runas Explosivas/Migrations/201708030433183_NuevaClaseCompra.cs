namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevaClaseCompra : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.DatosDeEnvios", newName: "TablaDatosDeEnvio");
            DropForeignKey("dbo.Compras", "Producto_ID", "dbo.Productos");
            DropIndex("dbo.Compras", new[] { "Producto_ID" });
            CreateTable(
                "dbo.ProductoEnCarritoes",
                c => new
                    {
                        Cantidad = c.Int(nullable: false, identity: true),
                        Producto_ID = c.Int(),
                        Compra_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Cantidad)
                .ForeignKey("dbo.Productos", t => t.Producto_ID)
                .ForeignKey("dbo.Compras", t => t.Compra_ID)
                .Index(t => t.Producto_ID)
                .Index(t => t.Compra_ID);
            
            AddColumn("dbo.Compras", "CompraAbonada", c => c.Boolean(nullable: false));
            AddColumn("dbo.Compras", "DatosDeEnvio_ID", c => c.Int());
            CreateIndex("dbo.Compras", "DatosDeEnvio_ID");
            AddForeignKey("dbo.Compras", "DatosDeEnvio_ID", "dbo.TablaDatosDeEnvio", "ID");
            DropColumn("dbo.Compras", "CantidadDeProducto");
            DropColumn("dbo.Compras", "Producto_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Compras", "Producto_ID", c => c.Int(nullable: false));
            AddColumn("dbo.Compras", "CantidadDeProducto", c => c.Int(nullable: false));
            DropForeignKey("dbo.ProductoEnCarritoes", "Compra_ID", "dbo.Compras");
            DropForeignKey("dbo.ProductoEnCarritoes", "Producto_ID", "dbo.Productos");
            DropForeignKey("dbo.Compras", "DatosDeEnvio_ID", "dbo.TablaDatosDeEnvio");
            DropIndex("dbo.ProductoEnCarritoes", new[] { "Compra_ID" });
            DropIndex("dbo.ProductoEnCarritoes", new[] { "Producto_ID" });
            DropIndex("dbo.Compras", new[] { "DatosDeEnvio_ID" });
            DropColumn("dbo.Compras", "DatosDeEnvio_ID");
            DropColumn("dbo.Compras", "CompraAbonada");
            DropTable("dbo.ProductoEnCarritoes");
            CreateIndex("dbo.Compras", "Producto_ID");
            AddForeignKey("dbo.Compras", "Producto_ID", "dbo.Productos", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.TablaDatosDeEnvio", newName: "DatosDeEnvios");
        }
    }
}
