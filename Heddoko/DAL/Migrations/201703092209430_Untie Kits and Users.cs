namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UntieKitsandUsers : DbMigration
    {
        public override void Up()
        {
            //Foreign key still tied to old Users table
            DropForeignKey("dbo.kits", "UserID", "dbo.Users");
            DropForeignKey("dbo.Kits", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Kits", new[] { "UserID" });
            DropColumn("dbo.Kits", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kits", "UserID", c => c.Int());
            CreateIndex("dbo.Kits", "UserID");
            AddForeignKey("dbo.Kits", "UserID", "dbo.AspNetUsers", "Id");
        }
    }
}
