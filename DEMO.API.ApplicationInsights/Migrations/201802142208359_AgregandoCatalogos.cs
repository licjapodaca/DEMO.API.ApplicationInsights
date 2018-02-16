namespace DEMO.API.ApplicationInsights.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregandoCatalogos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehiculoMarcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreMarca = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VehiculoSubmarcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehiculoMarcaId = c.Int(nullable: false),
                        NombreSubmarca = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehiculoMarcas", t => t.VehiculoMarcaId, cascadeDelete: true)
                .Index(t => t.VehiculoMarcaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehiculoSubmarcas", "VehiculoMarcaId", "dbo.VehiculoMarcas");
            DropIndex("dbo.VehiculoSubmarcas", new[] { "VehiculoMarcaId" });
            DropTable("dbo.VehiculoSubmarcas");
            DropTable("dbo.VehiculoMarcas");
        }
    }
}
