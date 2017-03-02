/**
 * @file 201610200954010_RemoveUserTokens.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUserTokens : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ConfirmToken");
            DropColumn("dbo.AspNetUsers", "RememberToken");
            DropColumn("dbo.AspNetUsers", "ForgotToken");
            DropColumn("dbo.AspNetUsers", "InviteToken");
            DropColumn("dbo.AspNetUsers", "ForgotExpiration");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ForgotExpiration", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "InviteToken", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "ForgotToken", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "RememberToken", c => c.String(maxLength: 100));
            AddColumn("dbo.AspNetUsers", "ConfirmToken", c => c.String(maxLength: 100));
        }
    }
}
