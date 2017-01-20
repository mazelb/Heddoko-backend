/**
 * @file 201607221948075_Firmware.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Firmware : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Firmwares",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Version = c.String(maxLength: 255),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        AssetID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.AssetID)
                .Index(t => t.AssetID);
            
            AddColumn("dbo.Brainpacks", "FirmwareID", c => c.Int());
            AddColumn("dbo.Databoards", "FirmwareID", c => c.Int());
            AddColumn("dbo.Powerboards", "FirmwareID", c => c.Int());
            CreateIndex("dbo.Brainpacks", "FirmwareID");
            CreateIndex("dbo.Databoards", "FirmwareID");
            CreateIndex("dbo.Powerboards", "FirmwareID");
            AddForeignKey("dbo.Databoards", "FirmwareID", "dbo.Firmwares", "ID");
            AddForeignKey("dbo.Brainpacks", "FirmwareID", "dbo.Firmwares", "ID");
            AddForeignKey("dbo.Powerboards", "FirmwareID", "dbo.Firmwares", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Powerboards", "FirmwareID", "dbo.Firmwares");
            DropForeignKey("dbo.Brainpacks", "FirmwareID", "dbo.Firmwares");
            DropForeignKey("dbo.Databoards", "FirmwareID", "dbo.Firmwares");
            DropForeignKey("dbo.Firmwares", "AssetID", "dbo.Assets");
            DropIndex("dbo.Powerboards", new[] { "FirmwareID" });
            DropIndex("dbo.Firmwares", new[] { "AssetID" });
            DropIndex("dbo.Databoards", new[] { "FirmwareID" });
            DropIndex("dbo.Brainpacks", new[] { "FirmwareID" });
            DropColumn("dbo.Powerboards", "FirmwareID");
            DropColumn("dbo.Databoards", "FirmwareID");
            DropColumn("dbo.Brainpacks", "FirmwareID");
            DropTable("dbo.Firmwares");
        }
    }
}
