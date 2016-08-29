namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoveToNvarcharMax : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Kits", "Notes", c => c.String());
            AlterColumn("dbo.Brainpacks", "Notes", c => c.String());
            AlterColumn("dbo.Databoards", "Notes", c => c.String());
            AlterColumn("dbo.Powerboards", "Notes", c => c.String());
            AlterColumn("dbo.Organizations", "Notes", c => c.String());
            AlterColumn("dbo.Pants", "Notes", c => c.String());
            AlterColumn("dbo.PantsOctopis", "Notes", c => c.String());
            AlterColumn("dbo.SensorSets", "Notes", c => c.String());
            AlterColumn("dbo.Sensors", "Notes", c => c.String());
            AlterColumn("dbo.Shirts", "Notes", c => c.String());
            AlterColumn("dbo.ShirtOctopis", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ShirtOctopis", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Shirts", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Sensors", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.SensorSets", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.PantsOctopis", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Pants", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Organizations", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Powerboards", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Databoards", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Brainpacks", "Notes", c => c.String(storeType: "ntext"));
            AlterColumn("dbo.Kits", "Notes", c => c.String(storeType: "ntext"));
        }
    }
}
