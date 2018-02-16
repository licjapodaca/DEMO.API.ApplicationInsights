namespace DEMO.API.ApplicationInsights.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdecuacionesCatalogos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehiculoMarcas", "NombreMarca", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehiculoMarcas", "NombreMarca", c => c.String());
        }
    }
}
