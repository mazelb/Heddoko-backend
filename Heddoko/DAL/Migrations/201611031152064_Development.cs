namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Development : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Developments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Client = c.String(maxLength: 255),
                        Secret = c.String(maxLength: 255),
                        Enabled = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Developments");
        }
    }
}
