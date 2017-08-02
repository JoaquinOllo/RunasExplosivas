namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDaCompra : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Compras", "Comprador_Mail", "dbo.Usuarios");
            DropForeignKey("dbo.Compras", "Producto_ID", "dbo.Productos");
            DropIndex("dbo.Compras", new[] { "Comprador_Mail" });
            DropIndex("dbo.Compras", new[] { "Producto_ID" });
            DropPrimaryKey("dbo.Compras");
            AddColumn("dbo.Compras", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Compras", "Comprador_Mail", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Compras", "Producto_ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Compras", "ID");
            CreateIndex("dbo.Compras", "Comprador_Mail");
            CreateIndex("dbo.Compras", "Producto_ID");
            AddForeignKey("dbo.Compras", "Comprador_Mail", "dbo.Usuarios", "Mail", cascadeDelete: true);
            AddForeignKey("dbo.Compras", "Producto_ID", "dbo.Productos", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Compras", "Producto_ID", "dbo.Productos");
            DropForeignKey("dbo.Compras", "Comprador_Mail", "dbo.Usuarios");
            DropIndex("dbo.Compras", new[] { "Producto_ID" });
            DropIndex("dbo.Compras", new[] { "Comprador_Mail" });
            DropPrimaryKey("dbo.Compras");
            AlterColumn("dbo.Compras", "Producto_ID", c => c.Int());
            AlterColumn("dbo.Compras", "Comprador_Mail", c => c.String(maxLength: 100));
            DropColumn("dbo.Compras", "ID");
            AddPrimaryKey("dbo.Compras", "Fecha");
            CreateIndex("dbo.Compras", "Producto_ID");
            CreateIndex("dbo.Compras", "Comprador_Mail");
            AddForeignKey("dbo.Compras", "Producto_ID", "dbo.Productos", "ID");
            AddForeignKey("dbo.Compras", "Comprador_Mail", "dbo.Usuarios", "Mail");
        }
    }
}
