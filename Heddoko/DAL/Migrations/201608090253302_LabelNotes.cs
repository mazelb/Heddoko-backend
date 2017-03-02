/**
 * @file 201608090253302_LabelNotes.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LabelNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kits", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.Kits", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Kits", "QAStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Brainpacks", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.Brainpacks", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Databoards", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.Databoards", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Databoards", "QAStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Powerboards", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.Powerboards", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Powerboards", "QAStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Pants", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.Pants", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.PantsOctopis", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.PantsOctopis", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.SensorSets", "Location", c => c.String(maxLength: 255));
            AddColumn("dbo.SensorSets", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.SensorSets", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Sensors", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.Sensors", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.Sensors", "FirmwareID", c => c.Int());
            AddColumn("dbo.Shirts", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.Shirts", "Notes", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ShirtOctopis", "Label", c => c.String(maxLength: 255));
            AddColumn("dbo.ShirtOctopis", "Notes", c => c.String(storeType: "ntext"));

            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.Kits(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.Brainpacks(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.Databoards(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.Powerboards(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.Pants(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.PantsOctopis(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.SensorSets(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.Sensors(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.Shirts(label) WHERE label IS NOT NULL;");
            Sql(@"CREATE UNIQUE NONCLUSTERED INDEX idx_label_notnull ON dbo.ShirtOctopis(label) WHERE label IS NOT NULL;");
         
            CreateIndex("dbo.Sensors", "FirmwareID");
            AddForeignKey("dbo.Sensors", "FirmwareID", "dbo.Firmwares", "ID");
            DropColumn("dbo.Sensors", "FirmwareVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensors", "FirmwareVersion", c => c.String(maxLength: 255));
            DropForeignKey("dbo.Sensors", "FirmwareID", "dbo.Firmwares");
            DropIndex("dbo.Sensors", new[] { "FirmwareID" });

            DropIndex("dbo.ShirtOctopis", "idx_label_notnull");
            DropIndex("dbo.Shirts", "idx_label_notnull");
            DropIndex("dbo.Sensors", "idx_label_notnull");
            DropIndex("dbo.SensorSets", "idx_label_notnull");
            DropIndex("dbo.PantsOctopis", "idx_label_notnull");
            DropIndex("dbo.Pants", "idx_label_notnull");
            DropIndex("dbo.Powerboards", "idx_label_notnull");
            DropIndex("dbo.Databoards", "idx_label_notnull");
            DropIndex("dbo.Brainpacks", "idx_label_notnull");
            DropIndex("dbo.Kits", "idx_label_notnull");

            DropColumn("dbo.ShirtOctopis", "Notes");
            DropColumn("dbo.ShirtOctopis", "Label");
            DropColumn("dbo.Shirts", "Notes");
            DropColumn("dbo.Shirts", "Label");
            DropColumn("dbo.Sensors", "FirmwareID");
            DropColumn("dbo.Sensors", "Notes");
            DropColumn("dbo.Sensors", "Label");
            DropColumn("dbo.SensorSets", "Notes");
            DropColumn("dbo.SensorSets", "Label");
            DropColumn("dbo.SensorSets", "Location");
            DropColumn("dbo.PantsOctopis", "Notes");
            DropColumn("dbo.PantsOctopis", "Label");
            DropColumn("dbo.Pants", "Notes");
            DropColumn("dbo.Pants", "Label");
            DropColumn("dbo.Powerboards", "QAStatus");
            DropColumn("dbo.Powerboards", "Notes");
            DropColumn("dbo.Powerboards", "Label");
            DropColumn("dbo.Databoards", "QAStatus");
            DropColumn("dbo.Databoards", "Notes");
            DropColumn("dbo.Databoards", "Label");
            DropColumn("dbo.Brainpacks", "Notes");
            DropColumn("dbo.Brainpacks", "Label");
            DropColumn("dbo.Kits", "QAStatus");
            DropColumn("dbo.Kits", "Notes");
            DropColumn("dbo.Kits", "Label");
        }
    }
}
