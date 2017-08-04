namespace Runas_Explosivas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormaDePagoAComprasAgregada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Compras", "FormaDePago", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Compras", "FormaDePago");
        }
    }
}
