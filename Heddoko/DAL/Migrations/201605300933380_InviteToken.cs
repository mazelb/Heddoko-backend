namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InviteToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "InviteToken", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "InviteToken");
        }
    }
}
