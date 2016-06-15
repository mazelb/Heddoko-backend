namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LicenseM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Licenses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ExpirationAt = c.DateTime(nullable: false),
                        OrganizationID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .Index(t => t.OrganizationID);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Phone = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                        Notes = c.String(storeType: "ntext"),
                        Status = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.Name, unique: true)
                .Index(t => t.UserID);
            
            AddColumn("dbo.Users", "OrganizationID", c => c.Int());
            AddColumn("dbo.Users", "LicenseID", c => c.Int());
            AddColumn("dbo.Users", "Organization_ID", c => c.Int());
            CreateIndex("dbo.Users", "OrganizationID");
            CreateIndex("dbo.Users", "LicenseID");
            CreateIndex("dbo.Users", "Organization_ID");
            AddForeignKey("dbo.Users", "Organization_ID", "dbo.Organizations", "ID");
            AddForeignKey("dbo.Users", "LicenseID", "dbo.Licenses", "ID");
            AddForeignKey("dbo.Users", "OrganizationID", "dbo.Organizations", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Users", "LicenseID", "dbo.Licenses");
            DropForeignKey("dbo.Users", "Organization_ID", "dbo.Organizations");
            DropForeignKey("dbo.Organizations", "UserID", "dbo.Users");
            DropForeignKey("dbo.Licenses", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.Organizations", new[] { "UserID" });
            DropIndex("dbo.Organizations", new[] { "Name" });
            DropIndex("dbo.Licenses", new[] { "OrganizationID" });
            DropIndex("dbo.Users", new[] { "Organization_ID" });
            DropIndex("dbo.Users", new[] { "LicenseID" });
            DropIndex("dbo.Users", new[] { "OrganizationID" });
            DropColumn("dbo.Users", "Organization_ID");
            DropColumn("dbo.Users", "LicenseID");
            DropColumn("dbo.Users", "OrganizationID");
            DropTable("dbo.Organizations");
            DropTable("dbo.Licenses");
        }
    }
}
