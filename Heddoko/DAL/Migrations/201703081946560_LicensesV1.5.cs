namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LicensesV15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Licenses", "TeamID", c => c.Int());
            CreateIndex("dbo.Licenses", "TeamID");
            AddForeignKey("dbo.Licenses", "TeamID", "dbo.Teams", "Id");
            DropColumn("dbo.Licenses", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Licenses", "Type", c => c.Int(nullable: false));
            DropForeignKey("dbo.Licenses", "TeamID", "dbo.Teams");
            DropIndex("dbo.Licenses", new[] { "TeamID" });
            DropColumn("dbo.Licenses", "TeamID");
        }
    }
}
