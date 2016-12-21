namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Application : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Developments", newName: "Applications");
            AddColumn("dbo.Applications", "RedirectUrl", c => c.String(maxLength: 2048));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applications", "RedirectUrl");
            RenameTable(name: "dbo.Applications", newName: "Developments");
        }
    }
}
