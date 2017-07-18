namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlyphRetiradoACategorias : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categorias", "Glyphicon");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categorias", "Glyphicon", c => c.String(maxLength: 50));
        }
    }
}
