/**
 * @file 201608090452536_AnatomicalLocation.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnatomicalLocation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sensors", "AnatomicalLocation", c => c.Int(nullable: false));
            DropColumn("dbo.Sensors", "AnatomicLocation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sensors", "AnatomicLocation", c => c.Int(nullable: false));
            DropColumn("dbo.Sensors", "AnatomicalLocation");
        }
    }
}
