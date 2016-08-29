namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Bitwise : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shirts", "QAStatus", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Shirts", "QAStatus", c => c.Int(nullable: false));
        }
    }
}
