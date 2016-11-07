namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserDevices : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(maxLength: 255),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Devices", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Devices", new[] { "UserID" });
            DropTable("dbo.Devices");
        }
    }
}
