namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecordToFirmware : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Firmwares", "RecordID", c => c.Int());
            CreateIndex("dbo.Firmwares", "RecordID");
            AddForeignKey("dbo.Firmwares", "RecordID", "dbo.Records", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Firmwares", "RecordID", "dbo.Records");
            DropIndex("dbo.Firmwares", new[] { "RecordID" });
            DropColumn("dbo.Firmwares", "RecordID");
        }
    }
}
