namespace Meteo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MeteoStationCardContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MeteoStationCards",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DateTime = c.DateTime(nullable: false),
                    Temperature = c.Int(nullable: false),
                    Humidity = c.Single(nullable: false),
                    Pressure = c.Single(nullable: false),
                    Radiation = c.Single(nullable: false),
                    WindSpeed = c.Single(nullable: false),
                    WindDirection = c.String(nullable: false),
                    Description = c.String(nullable: true),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.MeteoStationCards");
        }
    }
}
