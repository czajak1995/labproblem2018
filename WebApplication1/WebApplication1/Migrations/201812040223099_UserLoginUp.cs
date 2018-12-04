namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLoginUp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserLogins", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserLogins", "UserId");
        }
    }
}
