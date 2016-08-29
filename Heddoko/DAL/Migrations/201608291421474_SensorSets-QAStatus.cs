namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SensorSetsQAStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SensorSets", "QAStatus", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SensorSets", "QAStatus", c => c.Long(nullable: false));
        }
    }
}
