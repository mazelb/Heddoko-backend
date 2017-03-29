namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changing_upload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "BrainpackID", c => c.Int());
            AddColumn("dbo.Records", "BrainpackID", c => c.Int());
            CreateIndex("dbo.Assets", "BrainpackID");
            CreateIndex("dbo.Records", "BrainpackID");
            AddForeignKey("dbo.Records", "BrainpackID", "dbo.Brainpacks", "Id");
            AddForeignKey("dbo.Assets", "BrainpackID", "dbo.Brainpacks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "BrainpackID", "dbo.Brainpacks");
            DropForeignKey("dbo.Records", "BrainpackID", "dbo.Brainpacks");
            DropIndex("dbo.Records", new[] { "BrainpackID" });
            DropIndex("dbo.Assets", new[] { "BrainpackID" });
            DropColumn("dbo.Records", "BrainpackID");
            DropColumn("dbo.Assets", "BrainpackID");
        }
    }
}
