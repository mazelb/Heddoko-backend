namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOrganization : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Organization_ID" });
            CreateIndex("dbo.AspNetUsers", "Organization_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "Organization_Id" });
            CreateIndex("dbo.AspNetUsers", "Organization_ID");
        }
    }
}
