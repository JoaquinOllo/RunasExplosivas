namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductoModificada : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductoEnCarroes", "Compra_ID", "dbo.Compras");
            DropIndex("dbo.ProductoEnCarroes", new[] { "Compra_ID" });
            CreateTable(
                "dbo.ProductoEnCarroCompras",
                c => new
                    {
                        ProductoEnCarro_ID = c.Int(nullable: false),
                        Compra_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductoEnCarro_ID, t.Compra_ID })
                .ForeignKey("dbo.ProductoEnCarroes", t => t.ProductoEnCarro_ID, cascadeDelete: true)
                .ForeignKey("dbo.Compras", t => t.Compra_ID, cascadeDelete: true)
                .Index(t => t.ProductoEnCarro_ID)
                .Index(t => t.Compra_ID);
            
            DropColumn("dbo.ProductoEnCarroes", "Compra_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductoEnCarroes", "Compra_ID", c => c.Int());
            DropForeignKey("dbo.ProductoEnCarroCompras", "Compra_ID", "dbo.Compras");
            DropForeignKey("dbo.ProductoEnCarroCompras", "ProductoEnCarro_ID", "dbo.ProductoEnCarroes");
            DropIndex("dbo.ProductoEnCarroCompras", new[] { "Compra_ID" });
            DropIndex("dbo.ProductoEnCarroCompras", new[] { "ProductoEnCarro_ID" });
            DropTable("dbo.ProductoEnCarroCompras");
            CreateIndex("dbo.ProductoEnCarroes", "Compra_ID");
            AddForeignKey("dbo.ProductoEnCarroes", "Compra_ID", "dbo.Compras", "ID");
        }
    }
}
