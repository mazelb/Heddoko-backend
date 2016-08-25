using System.Data.Entity.Migrations;

namespace DAL.Migrations
{
    public partial class RemoveOld : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Groups", "TagID", "dbo.Tags");
            DropForeignKey("dbo.Groups", "AssetID", "dbo.Assets");
            DropForeignKey("dbo.GroupUsers", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.GroupUsers", "User_ID", "dbo.Users");
            DropForeignKey("dbo.GroupTags", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.ProfileGroups", "Group_ID", "dbo.Groups");
            DropForeignKey("dbo.Profiles", "AssetID", "dbo.Assets");
            DropForeignKey("dbo.Folders", "ParentID", "dbo.Folders");
            DropForeignKey("dbo.MovementFrames", "MovementID", "dbo.Movements");
            DropForeignKey("dbo.Movements", "EndFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementEvents", "EndFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementEvents", "MovementID", "dbo.Movements");
            DropForeignKey("dbo.MovementEvents", "StartFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.Movements", "FolderID", "dbo.Folders");
            DropForeignKey("dbo.MovementFrames", "Movement_ID", "dbo.Movements");
            DropForeignKey("dbo.MovementMarkers", "EndFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementMarkers", "StartFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.MovementMarkers", "Movement_ID", "dbo.Movements");
            DropForeignKey("dbo.Movements", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Movements", "ScreeningID", "dbo.Screenings");
            DropForeignKey("dbo.Screenings", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Movements", "StartFrameID", "dbo.MovementFrames");
            DropForeignKey("dbo.Movements", "SubmittedByID", "dbo.Users");
            DropForeignKey("dbo.TagMovements", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.TagMovements", "Movement_ID", "dbo.Movements");
            DropForeignKey("dbo.Folders", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.ProfileGroups", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.ProfileUsers", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.ProfileUsers", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Profiles", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ProfileTags", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.ProfileTags", "Tag_ID", "dbo.Tags");
            DropForeignKey("dbo.GroupTags", "Tag_ID", "dbo.Tags");
            DropIndex("dbo.Groups",
                new[]
                {
                    "TagID"
                });
            DropIndex("dbo.Groups",
                new[]
                {
                    "AssetID"
                });
            DropIndex("dbo.Profiles",
                new[]
                {
                    "TagID"
                });
            DropIndex("dbo.Profiles",
                new[]
                {
                    "AssetID"
                });
            DropIndex("dbo.Folders",
                new[]
                {
                    "ParentID"
                });
            DropIndex("dbo.Folders",
                new[]
                {
                    "ProfileID"
                });
            DropIndex("dbo.Movements",
                new[]
                {
                    "StartFrameID"
                });
            DropIndex("dbo.Movements",
                new[]
                {
                    "EndFrameID"
                });
            DropIndex("dbo.Movements",
                new[]
                {
                    "SubmittedByID"
                });
            DropIndex("dbo.Movements",
                new[]
                {
                    "ProfileID"
                });
            DropIndex("dbo.Movements",
                new[]
                {
                    "ScreeningID"
                });
            DropIndex("dbo.Movements",
                new[]
                {
                    "FolderID"
                });
            DropIndex("dbo.MovementFrames",
                new[]
                {
                    "MovementID"
                });
            DropIndex("dbo.MovementFrames",
                new[]
                {
                    "Movement_ID"
                });
            DropIndex("dbo.MovementEvents",
                new[]
                {
                    "MovementID"
                });
            DropIndex("dbo.MovementEvents",
                new[]
                {
                    "StartFrameID"
                });
            DropIndex("dbo.MovementEvents",
                new[]
                {
                    "EndFrameID"
                });
            DropIndex("dbo.MovementMarkers",
                new[]
                {
                    "StartFrameID"
                });
            DropIndex("dbo.MovementMarkers",
                new[]
                {
                    "EndFrameID"
                });
            DropIndex("dbo.MovementMarkers",
                new[]
                {
                    "Movement_ID"
                });
            DropIndex("dbo.Screenings",
                new[]
                {
                    "ProfileID"
                });
            DropIndex("dbo.GroupUsers",
                new[]
                {
                    "Group_ID"
                });
            DropIndex("dbo.GroupUsers",
                new[]
                {
                    "User_ID"
                });
            DropIndex("dbo.TagMovements",
                new[]
                {
                    "Tag_ID"
                });
            DropIndex("dbo.TagMovements",
                new[]
                {
                    "Movement_ID"
                });
            DropIndex("dbo.ProfileGroups",
                new[]
                {
                    "Profile_ID"
                });
            DropIndex("dbo.ProfileGroups",
                new[]
                {
                    "Group_ID"
                });
            DropIndex("dbo.ProfileUsers",
                new[]
                {
                    "Profile_ID"
                });
            DropIndex("dbo.ProfileUsers",
                new[]
                {
                    "User_ID"
                });
            DropIndex("dbo.ProfileTags",
                new[]
                {
                    "Profile_ID"
                });
            DropIndex("dbo.ProfileTags",
                new[]
                {
                    "Tag_ID"
                });
            DropIndex("dbo.GroupTags",
                new[]
                {
                    "Group_ID"
                });
            DropIndex("dbo.GroupTags",
                new[]
                {
                    "Tag_ID"
                });
            DropTable("dbo.ProfileGroups");
            DropTable("dbo.ProfileUsers");
            DropTable("dbo.ProfileTags");
            DropTable("dbo.GroupUsers");
            DropTable("dbo.GroupTags");
            DropTable("dbo.TagMovements");
            DropTable("dbo.Groups");
            DropTable("dbo.Profiles");
            DropTable("dbo.Folders");
            DropTable("dbo.Movements");
            DropTable("dbo.MovementFrames");
            DropTable("dbo.MovementEvents");
            DropTable("dbo.MovementMarkers");
            DropTable("dbo.Screenings");
            DropTable("dbo.Tags");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.GroupTags",
                c => new
                {
                    Group_ID = c.Int(false),
                    Tag_ID = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Group_ID,
                    t.Tag_ID
                });

            CreateTable(
                "dbo.ProfileTags",
                c => new
                {
                    Profile_ID = c.Int(false),
                    Tag_ID = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Profile_ID,
                    t.Tag_ID
                });

            CreateTable(
                "dbo.ProfileUsers",
                c => new
                {
                    Profile_ID = c.Int(false),
                    User_ID = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Profile_ID,
                    t.User_ID
                });

            CreateTable(
                "dbo.ProfileGroups",
                c => new
                {
                    Profile_ID = c.Int(false),
                    Group_ID = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Profile_ID,
                    t.Group_ID
                });

            CreateTable(
                "dbo.TagMovements",
                c => new
                {
                    Tag_ID = c.Int(false),
                    Movement_ID = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Tag_ID,
                    t.Movement_ID
                });

            CreateTable(
                "dbo.GroupUsers",
                c => new
                {
                    Group_ID = c.Int(false),
                    User_ID = c.Int(false)
                })
                .PrimaryKey(t => new
                {
                    t.Group_ID,
                    t.User_ID
                });

            CreateTable(
                "dbo.Tags",
                c => new
                {
                    ID = c.Int(false, true),
                    Title = c.String(maxLength: 100),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Screenings",
                c => new
                {
                    ID = c.Int(false, true),
                    Title = c.String(maxLength: 255),
                    Score = c.Boolean(),
                    ScoreMin = c.Boolean(),
                    ScoreMax = c.Boolean(),
                    Notes = c.String(storeType: "ntext"),
                    Meta = c.String(storeType: "ntext"),
                    ProfileID = c.Int(),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.MovementMarkers",
                c => new
                {
                    ID = c.Int(false, true),
                    Comments = c.String(maxLength: 255),
                    StartFrameID = c.Int(),
                    EndFrameID = c.Int(),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false),
                    Movement_ID = c.Int()
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.MovementEvents",
                c => new
                {
                    ID = c.Int(false, true),
                    Data = c.String(storeType: "ntext"),
                    Type = c.Int(false),
                    MovementID = c.Int(false),
                    StartFrameID = c.Int(),
                    EndFrameID = c.Int(),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.MovementFrames",
                c => new
                {
                    ID = c.Int(false, true),
                    Revision = c.String(maxLength: 255),
                    MovementID = c.Int(false),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false),
                    Movement_ID = c.Int()
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Movements",
                c => new
                {
                    ID = c.Int(false, true),
                    Title = c.String(maxLength: 255),
                    Notes = c.String(storeType: "ntext"),
                    Data = c.String(storeType: "ntext"),
                    Score = c.Boolean(),
                    ScoreMin = c.Boolean(),
                    ScroreMax = c.Boolean(),
                    StartFrameID = c.Int(),
                    EndFrameID = c.Int(),
                    SubmittedByID = c.Int(),
                    ProfileID = c.Int(),
                    ScreeningID = c.Int(),
                    FolderID = c.Int(),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Folders",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(maxLength: 255),
                    SystemName = c.String(maxLength: 255),
                    Path = c.String(maxLength: 255),
                    ParentID = c.Int(),
                    ProfileID = c.Int(false),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Profiles",
                c => new
                {
                    ID = c.Int(false, true),
                    Email = c.String(maxLength: 255),
                    FirstName = c.String(maxLength: 255),
                    LastName = c.String(maxLength: 255),
                    Phone = c.String(maxLength: 255),
                    Height = c.Double(),
                    Weight = c.Double(),
                    BirthDay = c.DateTime(),
                    Gender = c.Int(false),
                    Data = c.String(storeType: "ntext"),
                    TagID = c.Int(),
                    AssetID = c.Int(),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Groups",
                c => new
                {
                    ID = c.Int(false, true),
                    Name = c.String(maxLength: 255),
                    Meta = c.String(storeType: "ntext"),
                    TagID = c.Int(),
                    AssetID = c.Int(),
                    Updated = c.DateTime(),
                    Created = c.DateTime(false)
                })
                .PrimaryKey(t => t.ID);

            CreateIndex("dbo.GroupTags", "Tag_ID");
            CreateIndex("dbo.GroupTags", "Group_ID");
            CreateIndex("dbo.ProfileTags", "Tag_ID");
            CreateIndex("dbo.ProfileTags", "Profile_ID");
            CreateIndex("dbo.ProfileUsers", "User_ID");
            CreateIndex("dbo.ProfileUsers", "Profile_ID");
            CreateIndex("dbo.ProfileGroups", "Group_ID");
            CreateIndex("dbo.ProfileGroups", "Profile_ID");
            CreateIndex("dbo.TagMovements", "Movement_ID");
            CreateIndex("dbo.TagMovements", "Tag_ID");
            CreateIndex("dbo.GroupUsers", "User_ID");
            CreateIndex("dbo.GroupUsers", "Group_ID");
            CreateIndex("dbo.Screenings", "ProfileID");
            CreateIndex("dbo.MovementMarkers", "Movement_ID");
            CreateIndex("dbo.MovementMarkers", "EndFrameID");
            CreateIndex("dbo.MovementMarkers", "StartFrameID");
            CreateIndex("dbo.MovementEvents", "EndFrameID");
            CreateIndex("dbo.MovementEvents", "StartFrameID");
            CreateIndex("dbo.MovementEvents", "MovementID");
            CreateIndex("dbo.MovementFrames", "Movement_ID");
            CreateIndex("dbo.MovementFrames", "MovementID");
            CreateIndex("dbo.Movements", "FolderID");
            CreateIndex("dbo.Movements", "ScreeningID");
            CreateIndex("dbo.Movements", "ProfileID");
            CreateIndex("dbo.Movements", "SubmittedByID");
            CreateIndex("dbo.Movements", "EndFrameID");
            CreateIndex("dbo.Movements", "StartFrameID");
            CreateIndex("dbo.Folders", "ProfileID");
            CreateIndex("dbo.Folders", "ParentID");
            CreateIndex("dbo.Profiles", "AssetID");
            CreateIndex("dbo.Profiles", "TagID");
            CreateIndex("dbo.Groups", "AssetID");
            CreateIndex("dbo.Groups", "TagID");
            AddForeignKey("dbo.GroupTags", "Tag_ID", "dbo.Tags", "ID", true);
            AddForeignKey("dbo.GroupTags", "Group_ID", "dbo.Groups", "ID", true);
            AddForeignKey("dbo.Groups", "TagID", "dbo.Tags", "ID");
            AddForeignKey("dbo.ProfileTags", "Tag_ID", "dbo.Tags", "ID", true);
            AddForeignKey("dbo.ProfileTags", "Profile_ID", "dbo.Profiles", "ID", true);
            AddForeignKey("dbo.Profiles", "TagID", "dbo.Tags", "ID");
            AddForeignKey("dbo.ProfileUsers", "User_ID", "dbo.Users", "ID", true);
            AddForeignKey("dbo.ProfileUsers", "Profile_ID", "dbo.Profiles", "ID", true);
            AddForeignKey("dbo.ProfileGroups", "Group_ID", "dbo.Groups", "ID", true);
            AddForeignKey("dbo.ProfileGroups", "Profile_ID", "dbo.Profiles", "ID", true);
            AddForeignKey("dbo.Folders", "ProfileID", "dbo.Profiles", "ID");
            AddForeignKey("dbo.TagMovements", "Movement_ID", "dbo.Movements", "ID", true);
            AddForeignKey("dbo.TagMovements", "Tag_ID", "dbo.Tags", "ID", true);
            AddForeignKey("dbo.Movements", "SubmittedByID", "dbo.Users", "ID");
            AddForeignKey("dbo.Movements", "StartFrameID", "dbo.MovementFrames", "ID");
            AddForeignKey("dbo.Screenings", "ProfileID", "dbo.Profiles", "ID");
            AddForeignKey("dbo.Movements", "ScreeningID", "dbo.Screenings", "ID");
            AddForeignKey("dbo.Movements", "ProfileID", "dbo.Profiles", "ID");
            AddForeignKey("dbo.MovementMarkers", "Movement_ID", "dbo.Movements", "ID");
            AddForeignKey("dbo.MovementMarkers", "StartFrameID", "dbo.MovementFrames", "ID");
            AddForeignKey("dbo.MovementMarkers", "EndFrameID", "dbo.MovementFrames", "ID");
            AddForeignKey("dbo.MovementFrames", "Movement_ID", "dbo.Movements", "ID");
            AddForeignKey("dbo.Movements", "FolderID", "dbo.Folders", "ID");
            AddForeignKey("dbo.MovementEvents", "StartFrameID", "dbo.MovementFrames", "ID");
            AddForeignKey("dbo.MovementEvents", "MovementID", "dbo.Movements", "ID");
            AddForeignKey("dbo.MovementEvents", "EndFrameID", "dbo.MovementFrames", "ID");
            AddForeignKey("dbo.Movements", "EndFrameID", "dbo.MovementFrames", "ID");
            AddForeignKey("dbo.MovementFrames", "MovementID", "dbo.Movements", "ID");
            AddForeignKey("dbo.Folders", "ParentID", "dbo.Folders", "ID");
            AddForeignKey("dbo.Profiles", "AssetID", "dbo.Assets", "ID");
            AddForeignKey("dbo.GroupUsers", "User_ID", "dbo.Users", "ID", true);
            AddForeignKey("dbo.GroupUsers", "Group_ID", "dbo.Groups", "ID", true);
            AddForeignKey("dbo.Groups", "AssetID", "dbo.Assets", "ID");
        }
    }
}