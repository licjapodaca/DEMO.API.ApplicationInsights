namespace DEMO.API.ApplicationInsights.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OtrasAdecuaciones : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.VehiculoSubmarcas", new[] { "NombreSubmarca" });
            CreateIndex("dbo.VehiculoSubmarcas", "NombreSubmarca", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.VehiculoSubmarcas", new[] { "NombreSubmarca" });
            CreateIndex("dbo.VehiculoSubmarcas", "NombreSubmarca", unique: true);
        }
    }
}
