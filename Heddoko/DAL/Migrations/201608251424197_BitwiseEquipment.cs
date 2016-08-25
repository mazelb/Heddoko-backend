namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BitwiseEquipment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brainpacks", "QAStatus", c => c.Long(nullable: false));

            Sql("ALTER TABLE dbo.Databoards DROP CONSTRAINT DF__Databoard__QASta__2077C861");
            AlterColumn("dbo.Databoards", "QAStatus", c => c.Long(nullable: false));

            Sql("ALTER TABLE dbo.Powerboards DROP CONSTRAINT DF__Powerboar__QASta__216BEC9A");
            AlterColumn("dbo.Powerboards", "QAStatus", c => c.Long(nullable: false));


            AlterColumn("dbo.Pants", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.PantsOctopis", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.Sensors", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.ShirtOctopis", "QAStatus", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShirtOctopis", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Sensors", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.PantsOctopis", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Pants", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Powerboards", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Databoards", "QAStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Brainpacks", "QAStatus", c => c.Int(nullable: false));
        }
    }
}