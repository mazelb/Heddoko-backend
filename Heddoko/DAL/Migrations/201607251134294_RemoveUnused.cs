namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnused : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Databoards", "FirmwareVersion");
            DropColumn("dbo.Powerboards", "FirmwareVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Powerboards", "FirmwareVersion", c => c.String(maxLength: 255));
            AddColumn("dbo.Databoards", "FirmwareVersion", c => c.String(maxLength: 255));
        }
    }
}
