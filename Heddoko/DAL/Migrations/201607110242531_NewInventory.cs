namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewInventory : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroupProfiles", newName: "ProfileGroups");
            DropForeignKey("dbo.Equipments", "ComplexEquipmentID", "dbo.ComplexEquipments");
            DropForeignKey("dbo.Movements", "ComplexEquipmentID", "dbo.ComplexEquipments");
            DropForeignKey("dbo.Materials", "MaterialTypeID", "dbo.MaterialTypes");
            DropForeignKey("dbo.Equipments", "MaterialID", "dbo.Materials");
            DropForeignKey("dbo.Equipments", "VerifiedByID", "dbo.Users");
            DropIndex("dbo.Equipments", new[] { "MacAddress" });
            DropIndex("dbo.Equipments", new[] { "SerialNo" });
            DropIndex("dbo.Equipments", new[] { "ComplexEquipmentID" });
            DropIndex("dbo.Equipments", new[] { "MaterialID" });
            DropIndex("dbo.Equipments", new[] { "VerifiedByID" });
            DropIndex("dbo.ComplexEquipments", new[] { "MacAddress" });
            DropIndex("dbo.ComplexEquipments", new[] { "SerialNo" });
            DropIndex("dbo.Movements", new[] { "ComplexEquipmentID" });
            DropIndex("dbo.Materials", new[] { "Name" });
            DropIndex("dbo.Materials", new[] { "PartNo" });
            DropIndex("dbo.Materials", new[] { "MaterialTypeID" });
            DropIndex("dbo.MaterialTypes", new[] { "Identifier" });
            DropPrimaryKey("dbo.ProfileGroups");
            CreateTable(
                "dbo.Kits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Composition = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        OrganizationID = c.Int(),
                        BrainpackID = c.Int(),
                        SensorSetID = c.Int(),
                        ShirtID = c.Int(),
                        PantsID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brainpacks", t => t.BrainpackID)
                .ForeignKey("dbo.Organizations", t => t.OrganizationID)
                .ForeignKey("dbo.Pants", t => t.PantsID)
                .ForeignKey("dbo.SensorSets", t => t.SensorSetID)
                .ForeignKey("dbo.Shirts", t => t.ShirtID)
                .Index(t => t.OrganizationID)
                .Index(t => t.BrainpackID)
                .Index(t => t.SensorSetID)
                .Index(t => t.ShirtID)
                .Index(t => t.PantsID);
            
            CreateTable(
                "dbo.Brainpacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Version = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        QAStatus = c.Int(nullable: false),
                        PowerboardID = c.Int(),
                        DataboardID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Databoards", t => t.DataboardID)
                .ForeignKey("dbo.Powerboards", t => t.PowerboardID)
                .Index(t => t.PowerboardID)
                .Index(t => t.DataboardID);
            
            CreateTable(
                "dbo.Databoards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Version = c.String(maxLength: 255),
                        FirmwareVersion = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Powerboards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Version = c.String(maxLength: 255),
                        FirmwareVersion = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Size = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        QAStatus = c.Int(nullable: false),
                        PantsOctopiID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PantsOctopis", t => t.PantsOctopiID)
                .Index(t => t.PantsOctopiID);
            
            CreateTable(
                "dbo.PantsOctopis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Size = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        QAStatus = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SensorSets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QAStatus = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Sensors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Location = c.String(maxLength: 255),
                        Version = c.String(maxLength: 255),
                        FirmwareVersion = c.String(maxLength: 255),
                        Status = c.Int(nullable: false),
                        QAStatus = c.Int(nullable: false),
                        AnatomicLocation = c.Int(nullable: false),
                        SensorSetID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SensorSets", t => t.SensorSetID)
                .Index(t => t.SensorSetID);
            
            CreateTable(
                "dbo.Shirts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Size = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        QAStatus = c.Int(nullable: false),
                        ShirtOctopiID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ShirtOctopis", t => t.ShirtOctopiID)
                .Index(t => t.ShirtOctopiID);
            
            CreateTable(
                "dbo.ShirtOctopis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Size = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        QAStatus = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(maxLength: 255),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddPrimaryKey("dbo.ProfileGroups", new[] { "Profile_ID", "Group_ID" });
            DropColumn("dbo.Movements", "ComplexEquipmentID");
            DropTable("dbo.Equipments");
            DropTable("dbo.ComplexEquipments");
            DropTable("dbo.Materials");
            DropTable("dbo.MaterialTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MaterialTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Identifier = c.String(maxLength: 50),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        PartNo = c.String(maxLength: 255),
                        MaterialTypeID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ComplexEquipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        MacAddress = c.String(maxLength: 255),
                        SerialNo = c.String(maxLength: 255),
                        PhysicalLocation = c.String(maxLength: 255),
                        Notes = c.String(storeType: "ntext"),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        AnatomicalPosition = c.Int(),
                        Prototype = c.Int(nullable: false),
                        Condition = c.Int(nullable: false),
                        Numbers = c.Int(nullable: false),
                        HeatsShrink = c.Int(nullable: false),
                        Ship = c.Int(nullable: false),
                        MacAddress = c.String(maxLength: 255),
                        SerialNo = c.String(maxLength: 255),
                        PhysicalLocation = c.String(maxLength: 255),
                        Notes = c.String(storeType: "ntext"),
                        ComplexEquipmentID = c.Int(),
                        MaterialID = c.Int(nullable: false),
                        VerifiedByID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Movements", "ComplexEquipmentID", c => c.Int());
            DropForeignKey("dbo.Shirts", "ShirtOctopiID", "dbo.ShirtOctopis");
            DropForeignKey("dbo.Kits", "ShirtID", "dbo.Shirts");
            DropForeignKey("dbo.Sensors", "SensorSetID", "dbo.SensorSets");
            DropForeignKey("dbo.Kits", "SensorSetID", "dbo.SensorSets");
            DropForeignKey("dbo.Pants", "PantsOctopiID", "dbo.PantsOctopis");
            DropForeignKey("dbo.Kits", "PantsID", "dbo.Pants");
            DropForeignKey("dbo.Kits", "OrganizationID", "dbo.Organizations");
            DropForeignKey("dbo.Brainpacks", "PowerboardID", "dbo.Powerboards");
            DropForeignKey("dbo.Kits", "BrainpackID", "dbo.Brainpacks");
            DropForeignKey("dbo.Brainpacks", "DataboardID", "dbo.Databoards");
            DropIndex("dbo.Shirts", new[] { "ShirtOctopiID" });
            DropIndex("dbo.Sensors", new[] { "SensorSetID" });
            DropIndex("dbo.Pants", new[] { "PantsOctopiID" });
            DropIndex("dbo.Brainpacks", new[] { "DataboardID" });
            DropIndex("dbo.Brainpacks", new[] { "PowerboardID" });
            DropIndex("dbo.Kits", new[] { "PantsID" });
            DropIndex("dbo.Kits", new[] { "ShirtID" });
            DropIndex("dbo.Kits", new[] { "SensorSetID" });
            DropIndex("dbo.Kits", new[] { "BrainpackID" });
            DropIndex("dbo.Kits", new[] { "OrganizationID" });
            DropPrimaryKey("dbo.ProfileGroups");
            DropTable("dbo.Components");
            DropTable("dbo.ShirtOctopis");
            DropTable("dbo.Shirts");
            DropTable("dbo.Sensors");
            DropTable("dbo.SensorSets");
            DropTable("dbo.PantsOctopis");
            DropTable("dbo.Pants");
            DropTable("dbo.Powerboards");
            DropTable("dbo.Databoards");
            DropTable("dbo.Brainpacks");
            DropTable("dbo.Kits");
            AddPrimaryKey("dbo.ProfileGroups", new[] { "Group_ID", "Profile_ID" });
            CreateIndex("dbo.MaterialTypes", "Identifier", unique: true);
            CreateIndex("dbo.Materials", "MaterialTypeID");
            CreateIndex("dbo.Materials", "PartNo", unique: true);
            CreateIndex("dbo.Materials", "Name", unique: true);
            CreateIndex("dbo.Movements", "ComplexEquipmentID");
            CreateIndex("dbo.ComplexEquipments", "SerialNo", unique: true);
            CreateIndex("dbo.ComplexEquipments", "MacAddress", unique: true);
            CreateIndex("dbo.Equipments", "VerifiedByID");
            CreateIndex("dbo.Equipments", "MaterialID");
            CreateIndex("dbo.Equipments", "ComplexEquipmentID");
            CreateIndex("dbo.Equipments", "SerialNo", unique: true);
            CreateIndex("dbo.Equipments", "MacAddress", unique: true);
            AddForeignKey("dbo.Equipments", "VerifiedByID", "dbo.Users", "ID");
            AddForeignKey("dbo.Equipments", "MaterialID", "dbo.Materials", "ID");
            AddForeignKey("dbo.Materials", "MaterialTypeID", "dbo.MaterialTypes", "ID");
            AddForeignKey("dbo.Movements", "ComplexEquipmentID", "dbo.ComplexEquipments", "ID");
            AddForeignKey("dbo.Equipments", "ComplexEquipmentID", "dbo.ComplexEquipments", "ID");
            RenameTable(name: "dbo.ProfileGroups", newName: "GroupProfiles");
        }
    }
}
