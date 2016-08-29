namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SensorNullableAnatomicaLocation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sensors", "AnatomicalLocation", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sensors", "AnatomicalLocation", c => c.Int(nullable: false));
        }
    }
}
