/**
 * @file 201609091114388_Team.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Team : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                        Notes = c.String(),
                        Status = c.Int(nullable: false),
                        OrganizationID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .Index(t => t.Name)
                .Index(t => t.OrganizationID);
            
            AddColumn("dbo.Users", "TeamID", c => c.Int());
            CreateIndex("dbo.Users", "TeamID");
            AddForeignKey("dbo.Users", "TeamID", "dbo.Teams", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "TeamID", "dbo.Teams");
            DropForeignKey("dbo.Teams", "OrganizationID", "dbo.Organizations");
            DropIndex("dbo.Teams", new[] { "OrganizationID" });
            DropIndex("dbo.Teams", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "TeamID" });
            DropColumn("dbo.Users", "TeamID");
            DropTable("dbo.Teams");
        }
    }
}
