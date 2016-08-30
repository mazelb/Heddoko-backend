namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BitwiseQaStatusNvarcharNullableAnatomicalLocation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kits", "Notes", c => c.String());
            AlterColumn("dbo.Brainpacks", "Notes", c => c.String());
            AlterColumn("dbo.Brainpacks", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.Databoards", "Notes", c => c.String());
            this.DeleteDefaultContraint("dbo.Databoards", "QAStatus");
            AlterColumn("dbo.Databoards", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.Powerboards", "Notes", c => c.String());
            this.DeleteDefaultContraint("dbo.Powerboards", "QAStatus");
            AlterColumn("dbo.Powerboards", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.Organizations", "Notes", c => c.String());
            AlterColumn("dbo.Pants", "Notes", c => c.String());
            AlterColumn("dbo.Pants", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.PantsOctopis", "Notes", c => c.String());
            AlterColumn("dbo.PantsOctopis", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.SensorSets", "Notes", c => c.String());
            AlterColumn("dbo.SensorSets", "QAStatus", c => c.Long());
            AlterColumn("dbo.Sensors", "Notes", c => c.String());
            AlterColumn("dbo.Sensors", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.Sensors", "AnatomicalLocation", c => c.Int());
            AlterColumn("dbo.Shirts", "Notes", c => c.String());
            AlterColumn("dbo.Shirts", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.ShirtOctopis", "Notes", c => c.String());
            AlterColumn("dbo.ShirtOctopis", "QAStatus", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShirtOctopis", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.ShirtOctopis", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Shirts", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Shirts", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Sensors", "AnatomicalLocation", c => c.Int(nullable: false));
            AlterColumn("dbo.Sensors", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Sensors", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.SensorSets", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.SensorSets", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.PantsOctopis", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.PantsOctopis", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Pants", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Pants", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Organizations", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Powerboards", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Powerboards", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Databoards", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Databoards", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Brainpacks", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Brainpacks", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Kits", "Notes", c => c.String(storeType: "ntext"));
        }
    }
}
