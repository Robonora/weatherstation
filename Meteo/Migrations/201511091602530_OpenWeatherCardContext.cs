namespace Meteo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OpenWeatherCardContext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OpenWeatherCards", "Humidity", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OpenWeatherCards", "Humidity", c => c.Int(nullable: false));
        }
    }
}
