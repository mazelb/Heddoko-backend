namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeinglicencefromuser : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "LicenseID", newName: "License_Id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_LicenseID", newName: "IX_License_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_License_Id", newName: "IX_LicenseID");
            RenameColumn(table: "dbo.AspNetUsers", name: "License_Id", newName: "LicenseID");
        }
    }
}
