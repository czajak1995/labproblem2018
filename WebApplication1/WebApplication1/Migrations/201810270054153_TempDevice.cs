namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempDevice : DbMigration
    {
        public override void Up()
        {
           CreateTable(
                "dbo.Temperatures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Temp = c.Int(nullable: false),
                        DeviceId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropColumn("dbo.Temperatures", "DeviceId");
            DropTable("dbo.Devices");
        }
    }
}
