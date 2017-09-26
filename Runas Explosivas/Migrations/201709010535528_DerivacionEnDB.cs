namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DerivacionEnDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComentariosEnArticulos", "Derivacion", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComentariosEnArticulos", "Derivacion");
        }
    }
}
