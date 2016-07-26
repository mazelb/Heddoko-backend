namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SensorSetStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kits", "UserID", c => c.Int());
            AddColumn("dbo.SensorSets", "Status", c => c.Int(nullable: false));
            CreateIndex("dbo.Kits", "UserID");
            AddForeignKey("dbo.Kits", "UserID", "dbo.Users", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Kits", "UserID", "dbo.Users");
            DropIndex("dbo.Kits", new[] { "UserID" });
            DropColumn("dbo.SensorSets", "Status");
            DropColumn("dbo.Kits", "UserID");
        }
    }
}
