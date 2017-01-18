/**
 * @file 201608152057200_AssetFilename.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssetFilename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "Name", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Assets", "Name");
        }
    }
}
