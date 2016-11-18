namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecord : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        KitID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kits", t => t.KitID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.KitID);
            
            AddColumn("dbo.Assets", "RecordID", c => c.Int());
            CreateIndex("dbo.Assets", "RecordID");
            AddForeignKey("dbo.Assets", "RecordID", "dbo.Records", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Records", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Records", "KitID", "dbo.Kits");
            DropForeignKey("dbo.Assets", "RecordID", "dbo.Records");
            DropIndex("dbo.Records", new[] { "KitID" });
            DropIndex("dbo.Records", new[] { "UserID" });
            DropIndex("dbo.Assets", new[] { "RecordID" });
            DropColumn("dbo.Assets", "RecordID");
            DropTable("dbo.Records");
        }
    }
}
