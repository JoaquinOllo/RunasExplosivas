namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CategoriaSinDescripcion : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categorias", "Descripcion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categorias", "Descripcion", c => c.String());
        }
    }
}
