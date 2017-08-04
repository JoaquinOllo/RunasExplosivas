namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PKIDAgregadaAProductoEnCarrito : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductoEnCarritoes", newName: "ProductoEnCarroes");
            DropForeignKey("dbo.ProductoEnCarroes", "Producto_ID", "dbo.Productos");
            DropIndex("dbo.ProductoEnCarroes", new[] { "Producto_ID" });
            DropPrimaryKey("dbo.ProductoEnCarroes");
            AddColumn("dbo.ProductoEnCarroes", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ProductoEnCarroes", "Cantidad", c => c.Int(nullable: false));
            AlterColumn("dbo.ProductoEnCarroes", "Producto_ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductoEnCarroes", "ID");
            CreateIndex("dbo.ProductoEnCarroes", "Producto_ID");
            AddForeignKey("dbo.ProductoEnCarroes", "Producto_ID", "dbo.Productos", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductoEnCarroes", "Producto_ID", "dbo.Productos");
            DropIndex("dbo.ProductoEnCarroes", new[] { "Producto_ID" });
            DropPrimaryKey("dbo.ProductoEnCarroes");
            AlterColumn("dbo.ProductoEnCarroes", "Producto_ID", c => c.Int());
            AlterColumn("dbo.ProductoEnCarroes", "Cantidad", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.ProductoEnCarroes", "ID");
            AddPrimaryKey("dbo.ProductoEnCarroes", "Cantidad");
            CreateIndex("dbo.ProductoEnCarroes", "Producto_ID");
            AddForeignKey("dbo.ProductoEnCarritoes", "Producto_ID", "dbo.Productos", "ID");
            RenameTable(name: "dbo.ProductoEnCarroes", newName: "ProductoEnCarritoes");
        }
    }
}
