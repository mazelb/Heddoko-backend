/**
 * @file 201607251134294_RemoveUnused.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnused : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Databoards", "FirmwareVersion");
            DropColumn("dbo.Powerboards", "FirmwareVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Powerboards", "FirmwareVersion", c => c.String(maxLength: 255));
            AddColumn("dbo.Databoards", "FirmwareVersion", c => c.String(maxLength: 255));
        }
    }
}
