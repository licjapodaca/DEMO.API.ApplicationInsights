namespace DEMO.API.ApplicationInsights.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MasAdecuacionesCatalogos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehiculoMarcas", "NombreMarca", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.VehiculoSubmarcas", "NombreSubmarca", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.VehiculoMarcas", "NombreMarca", unique: true);
            CreateIndex("dbo.VehiculoSubmarcas", "NombreSubmarca", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.VehiculoSubmarcas", new[] { "NombreSubmarca" });
            DropIndex("dbo.VehiculoMarcas", new[] { "NombreMarca" });
            AlterColumn("dbo.VehiculoSubmarcas", "NombreSubmarca", c => c.String());
            AlterColumn("dbo.VehiculoMarcas", "NombreMarca", c => c.String(nullable: false));
        }
    }
}
