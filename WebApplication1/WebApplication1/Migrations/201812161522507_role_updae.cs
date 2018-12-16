namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role_updae : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "UseMessanger", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Roles", "UseMessanger");
        }
    }
}
