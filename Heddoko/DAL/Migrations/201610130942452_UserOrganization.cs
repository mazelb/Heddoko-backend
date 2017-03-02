/**
 * @file 201610130942452_UserOrganization.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
