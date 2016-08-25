namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bitwiseeqipment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brainpacks", "QAStatus", c => c.Long(nullable: false));
            AlterColumn("dbo.Databoards", "QAStatus", c => c.Long(nullable: false));
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
