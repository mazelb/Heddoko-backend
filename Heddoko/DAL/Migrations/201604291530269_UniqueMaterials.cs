/**
 * @file 201604291530269_UniqueMaterials.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueMaterials : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Materials", "Name", unique: true);
            CreateIndex("dbo.Materials", "PartNo", unique: true);
            CreateIndex("dbo.MaterialTypes", "Identifier", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.MaterialTypes", new[] { "Identifier" });
            DropIndex("dbo.Materials", new[] { "PartNo" });
            DropIndex("dbo.Materials", new[] { "Name" });
        }
    }
}
