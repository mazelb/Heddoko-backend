namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultRecords : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Records", new[] { "UserID" });
            DropIndex("dbo.Records", new[] { "KitID" });
            AddColumn("dbo.Records", "Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Records", "UserID", c => c.Int());
            AlterColumn("dbo.Records", "KitID", c => c.Int());
            CreateIndex("dbo.Records", "UserID");
            CreateIndex("dbo.Records", "KitID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Records", new[] { "KitID" });
            DropIndex("dbo.Records", new[] { "UserID" });
            AlterColumn("dbo.Records", "KitID", c => c.Int(nullable: false));
            AlterColumn("dbo.Records", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Records", "Type");
            CreateIndex("dbo.Records", "KitID");
            CreateIndex("dbo.Records", "UserID");
        }
    }
}
