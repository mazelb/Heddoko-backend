/**
 * @file 201605300933380_InviteToken.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
