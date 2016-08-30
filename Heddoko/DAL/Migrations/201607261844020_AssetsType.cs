namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetsType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "AssetID", "dbo.Assets");
            DropIndex("dbo.Users", new[] { "AssetID" });
            AddColumn("dbo.Assets", "Proccessing", c => c.Int(nullable: false));
            AddColumn("dbo.Assets", "UserID", c => c.Int());
            AddColumn("dbo.Assets", "KitID", c => c.Int());
            CreateIndex("dbo.Assets", "UserID");
            CreateIndex("dbo.Assets", "KitID");
            AddForeignKey("dbo.Assets", "KitID", "dbo.Kits", "ID");
            AddForeignKey("dbo.Assets", "UserID", "dbo.Users", "ID");
            DropColumn("dbo.Users", "AssetID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "AssetID", c => c.Int());
            DropForeignKey("dbo.Assets", "UserID", "dbo.Users");
            DropForeignKey("dbo.Assets", "KitID", "dbo.Kits");
            DropIndex("dbo.Assets", new[] { "KitID" });
            DropIndex("dbo.Assets", new[] { "UserID" });
            DropColumn("dbo.Assets", "KitID");
            DropColumn("dbo.Assets", "UserID");
            DropColumn("dbo.Assets", "Proccessing");
            CreateIndex("dbo.Users", "AssetID");
            AddForeignKey("dbo.Users", "AssetID", "dbo.Assets", "ID");
        }
    }
}
