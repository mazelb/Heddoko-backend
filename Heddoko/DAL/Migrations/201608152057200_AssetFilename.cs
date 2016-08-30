namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetFilename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "Name", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assets", "Name");
        }
    }
}
