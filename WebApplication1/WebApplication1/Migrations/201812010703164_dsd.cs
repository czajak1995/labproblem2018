namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "Name", c => c.String());
            AlterColumn("dbo.UserLogins", "Date", c => c.String());
            AlterColumn("dbo.Users", "Forename", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Forename", c => c.String(nullable: false));
            AlterColumn("dbo.UserLogins", "Date", c => c.String(nullable: false));
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false));
        }
    }
}
