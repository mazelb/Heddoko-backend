namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessTokens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Token = c.String(maxLength: 100),
                        UserID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 255),
                        Username = c.String(maxLength: 255),
                        Password = c.String(maxLength: 100),
                        Salt = c.String(maxLength: 100),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        Phone = c.String(maxLength: 255),
                        Country = c.String(maxLength: 2),
                        Role = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ConfirmToken = c.String(maxLength: 100),
                        RememberToken = c.String(maxLength: 100),
                        ForgotToken = c.String(maxLength: 100),
                        ForgotExpiration = c.DateTime(),
                        AssetID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.AssetID)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Username, unique: true)
                .Index(t => t.AssetID);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 255),
                        Type = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Image);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ComplexEquipments", t => t.ComplexEquipmentID)
                .ForeignKey("dbo.Materials", t => t.MaterialID)
                .ForeignKey("dbo.Users", t => t.VerifiedByID)
                .Index(t => t.MacAddress, unique: true)
                .Index(t => t.SerialNo, unique: true)
                .Index(t => t.ComplexEquipmentID)
                .Index(t => t.MaterialID)
                .Index(t => t.VerifiedByID);
            
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
                .PrimaryKey(t => t.ID)
                .Index(t => t.MacAddress, unique: true)
                .Index(t => t.SerialNo, unique: true);
            
            CreateTable(
                "dbo.Movements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Notes = c.String(storeType: "ntext"),
                        Data = c.String(storeType: "ntext"),
                        Score = c.Boolean(),
                        ScoreMin = c.Boolean(),
                        ScroreMax = c.Boolean(),
                        StartFrameID = c.Int(),
                        EndFrameID = c.Int(),
                        ComplexEquipmentID = c.Int(),
                        SubmittedByID = c.Int(),
                        ProfileID = c.Int(),
                        ScreeningID = c.Int(),
                        FolderID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ComplexEquipments", t => t.ComplexEquipmentID)
                .ForeignKey("dbo.MovementFrames", t => t.EndFrameID)
                .ForeignKey("dbo.Folders", t => t.FolderID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .ForeignKey("dbo.Screenings", t => t.ScreeningID)
                .ForeignKey("dbo.MovementFrames", t => t.StartFrameID)
                .ForeignKey("dbo.Users", t => t.SubmittedByID)
                .Index(t => t.StartFrameID)
                .Index(t => t.EndFrameID)
                .Index(t => t.ComplexEquipmentID)
                .Index(t => t.SubmittedByID)
                .Index(t => t.ProfileID)
                .Index(t => t.ScreeningID)
                .Index(t => t.FolderID);
            
            CreateTable(
                "dbo.MovementFrames",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Revision = c.String(maxLength: 255),
                        MovementID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                        Movement_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movements", t => t.MovementID)
                .ForeignKey("dbo.Movements", t => t.Movement_ID)
                .Index(t => t.MovementID)
                .Index(t => t.Movement_ID);
            
            CreateTable(
                "dbo.MovementEvents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Data = c.String(storeType: "ntext"),
                        Type = c.Int(nullable: false),
                        MovementID = c.Int(nullable: false),
                        StartFrameID = c.Int(),
                        EndFrameID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MovementFrames", t => t.EndFrameID)
                .ForeignKey("dbo.Movements", t => t.MovementID)
                .ForeignKey("dbo.MovementFrames", t => t.StartFrameID)
                .Index(t => t.MovementID)
                .Index(t => t.StartFrameID)
                .Index(t => t.EndFrameID);
            
            CreateTable(
                "dbo.Folders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        SystemName = c.String(maxLength: 255),
                        Path = c.String(maxLength: 255),
                        ParentID = c.Int(),
                        ProfileID = c.Int(nullable: false),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Folders", t => t.ParentID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ParentID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 255),
                        FirstName = c.String(maxLength: 255),
                        LastName = c.String(maxLength: 255),
                        Phone = c.String(maxLength: 255),
                        Height = c.Double(),
                        Weight = c.Double(),
                        BirthDay = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        Data = c.String(storeType: "ntext"),
                        TagID = c.Int(),
                        AssetID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.AssetID)
                .ForeignKey("dbo.Tags", t => t.TagID)
                .Index(t => t.TagID)
                .Index(t => t.AssetID);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Meta = c.String(storeType: "ntext"),
                        TagID = c.Int(),
                        AssetID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Assets", t => t.AssetID)
                .ForeignKey("dbo.Tags", t => t.TagID)
                .Index(t => t.TagID)
                .Index(t => t.AssetID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Screenings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 255),
                        Score = c.Boolean(),
                        ScoreMin = c.Boolean(),
                        ScoreMax = c.Boolean(),
                        Notes = c.String(storeType: "ntext"),
                        Meta = c.String(storeType: "ntext"),
                        ProfileID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Profiles", t => t.ProfileID)
                .Index(t => t.ProfileID);
            
            CreateTable(
                "dbo.MovementMarkers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Comments = c.String(maxLength: 255),
                        StartFrameID = c.Int(),
                        EndFrameID = c.Int(),
                        Updated = c.DateTime(),
                        Created = c.DateTime(nullable: false),
                        Movement_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MovementFrames", t => t.EndFrameID)
                .ForeignKey("dbo.MovementFrames", t => t.StartFrameID)
                .ForeignKey("dbo.Movements", t => t.Movement_ID)
                .Index(t => t.StartFrameID)
                .Index(t => t.EndFrameID)
                .Index(t => t.Movement_ID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MaterialTypes", t => t.MaterialTypeID)
                .Index(t => t.MaterialTypeID);
            
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
                "dbo.GroupUsers",
                c => new
                    {
                        Group_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_ID, t.User_ID })
                .ForeignKey("dbo.Groups", t => t.Group_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.Group_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.GroupProfiles",
                c => new
                    {
                        Group_ID = c.Int(nullable: false),
                        Profile_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_ID, t.Profile_ID })
                .ForeignKey("dbo.Groups", t => t.Group_ID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.Profile_ID, cascadeDelete: true)
                .Index(t => t.Group_ID)
                .Index(t => t.Profile_ID);
            
            CreateTable(
                "dbo.TagMovements",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Movement_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Movement_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Movements", t => t.Movement_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Movement_ID);
            
            CreateTable(
                "dbo.GroupTags",
                c => new
                    {
                        Group_ID = c.Int(nullable: false),
                        Tag_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_ID, t.Tag_ID })
                .ForeignKey("dbo.Groups", t => t.Group_ID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .Index(t => t.Group_ID)
                .Index(t => t.Tag_ID);
            
            CreateTable(
                "dbo.ProfileUsers",
                c => new
                    {
                        Profile_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profile_ID, t.User_ID })
                .ForeignKey("dbo.Profiles", t => t.Profile_ID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.Profile_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.ProfileTags",
                c => new
                    {
                        Profile_ID = c.Int(nullable: false),
                        Tag_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profile_ID, t.Tag_ID })
                .ForeignKey("dbo.Profiles", t => t.Profile_ID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .Index(t => t.Profile_ID)
                .Index(t => t.Tag_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccessTokens", "UserID", "dbo.Users");
            DropForeignKey("dbo.Equipments", "VerifiedByID", "dbo.Users");
            DropForeignKey("dbo.Equipments", "MaterialID", "dbo.Materials");
            DropForeignKey("dbo.Materials", "MaterialTypeID", "dbo.MaterialTypes");
            DropForeignKey("dbo.Movements", "SubmittedByID", "dbo.Users");
            DropForeignKey("dbo.Movements", "StartFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementMarkers", "Movement_ID", "dbo.Movements");
            DropForeignKey("dbo.MovementMarkers", "StartFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementMarkers", "EndFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementFrames", "Movement_ID", "dbo.Movements");
            DropForeignKey("dbo.ProfileTags", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.ProfileTags", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "TagID", "dbo.Tags");
            DropForeignKey("dbo.Screenings", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Movements", "ScreeningID", "dbo.Screenings");
            DropForeignKey("dbo.Movements", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ProfileUsers", "User_ID", "dbo.Users");
            DropForeignKey("dbo.ProfileUsers", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.GroupTags", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.GroupTags", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "TagID", "dbo.Tags");
            DropForeignKey("dbo.TagMovements", "Movement_ID", "dbo.Movements");
            DropForeignKey("dbo.TagMovements", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.GroupProfiles", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.GroupProfiles", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.GroupUsers", "User_ID", "dbo.Users");
            DropForeignKey("dbo.GroupUsers", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.Groups", "AssetID", "dbo.Assets");
            DropForeignKey("dbo.Folders", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Profiles", "AssetID", "dbo.Assets");
            DropForeignKey("dbo.Movements", "FolderID", "dbo.Folders");
            DropForeignKey("dbo.Folders", "ParentID", "dbo.Folders");
            DropForeignKey("dbo.MovementEvents", "StartFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementEvents", "MovementID", "dbo.Movements");
            DropForeignKey("dbo.MovementEvents", "EndFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.Movements", "EndFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementFrames", "MovementID", "dbo.Movements");
            DropForeignKey("dbo.Movements", "ComplexEquipmentID", "dbo.ComplexEquipments");
            DropForeignKey("dbo.Equipments", "ComplexEquipmentID", "dbo.ComplexEquipments");
            DropForeignKey("dbo.Users", "AssetID", "dbo.Assets");
            DropIndex("dbo.ProfileTags", new[] { "Tag_ID" });
            DropIndex("dbo.ProfileTags", new[] { "Profile_ID" });
            DropIndex("dbo.ProfileUsers", new[] { "User_ID" });
            DropIndex("dbo.ProfileUsers", new[] { "Profile_ID" });
            DropIndex("dbo.GroupTags", new[] { "Tag_ID" });
            DropIndex("dbo.GroupTags", new[] { "Group_ID" });
            DropIndex("dbo.TagMovements", new[] { "Movement_ID" });
            DropIndex("dbo.TagMovements", new[] { "Tag_ID" });
            DropIndex("dbo.GroupProfiles", new[] { "Profile_ID" });
            DropIndex("dbo.GroupProfiles", new[] { "Group_ID" });
            DropIndex("dbo.GroupUsers", new[] { "User_ID" });
            DropIndex("dbo.GroupUsers", new[] { "Group_ID" });
            DropIndex("dbo.Materials", new[] { "MaterialTypeID" });
            DropIndex("dbo.MovementMarkers", new[] { "Movement_ID" });
            DropIndex("dbo.MovementMarkers", new[] { "EndFrameID" });
            DropIndex("dbo.MovementMarkers", new[] { "StartFrameID" });
            DropIndex("dbo.Screenings", new[] { "ProfileID" });
            DropIndex("dbo.Groups", new[] { "AssetID" });
            DropIndex("dbo.Groups", new[] { "TagID" });
            DropIndex("dbo.Profiles", new[] { "AssetID" });
            DropIndex("dbo.Profiles", new[] { "TagID" });
            DropIndex("dbo.Folders", new[] { "ProfileID" });
            DropIndex("dbo.Folders", new[] { "ParentID" });
            DropIndex("dbo.MovementEvents", new[] { "EndFrameID" });
            DropIndex("dbo.MovementEvents", new[] { "StartFrameID" });
            DropIndex("dbo.MovementEvents", new[] { "MovementID" });
            DropIndex("dbo.MovementFrames", new[] { "Movement_ID" });
            DropIndex("dbo.MovementFrames", new[] { "MovementID" });
            DropIndex("dbo.Movements", new[] { "FolderID" });
            DropIndex("dbo.Movements", new[] { "ScreeningID" });
            DropIndex("dbo.Movements", new[] { "ProfileID" });
            DropIndex("dbo.Movements", new[] { "SubmittedByID" });
            DropIndex("dbo.Movements", new[] { "ComplexEquipmentID" });
            DropIndex("dbo.Movements", new[] { "EndFrameID" });
            DropIndex("dbo.Movements", new[] { "StartFrameID" });
            DropIndex("dbo.ComplexEquipments", new[] { "SerialNo" });
            DropIndex("dbo.ComplexEquipments", new[] { "MacAddress" });
            DropIndex("dbo.Equipments", new[] { "VerifiedByID" });
            DropIndex("dbo.Equipments", new[] { "MaterialID" });
            DropIndex("dbo.Equipments", new[] { "ComplexEquipmentID" });
            DropIndex("dbo.Equipments", new[] { "SerialNo" });
            DropIndex("dbo.Equipments", new[] { "MacAddress" });
            DropIndex("dbo.Assets", new[] { "Image" });
            DropIndex("dbo.Users", new[] { "AssetID" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.AccessTokens", new[] { "UserID" });
            DropTable("dbo.ProfileTags");
            DropTable("dbo.ProfileUsers");
            DropTable("dbo.GroupTags");
            DropTable("dbo.TagMovements");
            DropTable("dbo.GroupProfiles");
            DropTable("dbo.GroupUsers");
            DropTable("dbo.MaterialTypes");
            DropTable("dbo.Materials");
            DropTable("dbo.MovementMarkers");
            DropTable("dbo.Screenings");
            DropTable("dbo.Tags");
            DropTable("dbo.Groups");
            DropTable("dbo.Profiles");
            DropTable("dbo.Folders");
            DropTable("dbo.MovementEvents");
            DropTable("dbo.MovementFrames");
            DropTable("dbo.Movements");
            DropTable("dbo.ComplexEquipments");
            DropTable("dbo.Equipments");
            DropTable("dbo.Assets");
            DropTable("dbo.Users");
            DropTable("dbo.AccessTokens");
        }
    }
}
