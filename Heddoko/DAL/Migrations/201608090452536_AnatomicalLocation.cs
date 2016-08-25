namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnatomicalLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "AnatomicalLocation", c => c.Int(nullable: false));
            DropColumn("dbo.Sensors", "AnatomicLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensors", "AnatomicLocation", c => c.Int(nullable: false));
            DropColumn("dbo.Sensors", "AnatomicalLocation");
        }
    }
}
