namespace Altitude.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeasurements : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Measurements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        Altitude = c.Double(nullable: false),
                        HorizontalAccuracy = c.Double(nullable: false),
                        VerticalAccuracy = c.Double(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Measurements");
        }
    }
}
