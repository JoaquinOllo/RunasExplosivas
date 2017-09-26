namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdenComentariosEnArticulo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articulos", "OrdenComentInternal", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articulos", "OrdenComentInternal");
        }
    }
}
