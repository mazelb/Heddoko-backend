namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DAL.Models;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<DAL.HDContext>
    {
        private UnitOfWork UoW { get; set; }

        public Configuration()
        {
            //TODO Disable it
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
            CommandTimeout = 360000;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        #region Get
        private User GetUser(string email)
        {
            return UoW.UserRepository.GetByEmail(email);
        }

        private Group GetGroup(string name)
        {
            return UoW.GroupRepository.GetByName(name);
        }

        private Material GetMaterial(string name)
        {
            return UoW.MaterialRepository.GetByName(name);
        }

        private MaterialType GetMaterialType(string name)
        {
            return UoW.MaterialTypeRepository.GetByName(name);
        }

        private int AddAsset(HDContext context, string image, AssetType type)
        {
            context.Assets.AddOrUpdate(
                p => p.Image,
                new Asset
                {
                    Image = image,
                    Type = type,
                    Status = UploadStatusType.Uploaded
                }
            );
            context.SaveChanges();

            Asset asset = UoW.AssetRepository.GetByImage(image);

            return asset.ID;
        }
        #endregion

        protected override void Seed(DAL.HDContext context)
        {
            if (Config.AllowInitData)
            {
                UoW = new UnitOfWork(context);
                Tags(context);
                Users(context);
                Groups(context);
                Profiles(context);
                Suits(context);
            }
        }

        private void Users(HDContext context)
        {
            Passphrase pwd = PasswordHasher.Hash("p@ssword");

            context.Users.AddOrUpdate(
                  p => p.Email,
                   new User
                   {
                       Email = "ss@a2a.co",
                       Username = "ss",
                       Status = UserStatusType.Active,
                       Password = pwd.Hash,
                       Salt = pwd.Salt,
                       FirstName = "S",
                       LastName = "S",
                       Role = UserRoleType.Admin
                   },
                   new User
                   {
                       Email = "admin@heddoko.co",
                       Username = "heddoko.admin",
                       Status = UserStatusType.Active,
                       Password = pwd.Hash,
                       Salt = pwd.Salt,
                       FirstName = "Admin",
                       LastName = "",
                       Role = UserRoleType.Admin
                   },
                   new User
                   {
                       Email = "support@heddoko.co",
                       Username = "heddoko.support",
                       Status = UserStatusType.Active,
                       Password = pwd.Hash,
                       Salt = pwd.Salt,
                       FirstName = "Support",
                       LastName = "",
                       Role = UserRoleType.Admin
                   },
                   new User
                   {
                       Email = "awesomeathlete111@heddoko.co",
                       Username = "awesomeathlete111",
                       Status = UserStatusType.Active,
                       Password = pwd.Hash,
                       Salt = pwd.Salt,
                       FirstName = "Awesome",
                       LastName = "Athlete",
                       Role = UserRoleType.User
                   },
                  new User
                  {
                      Email = "awesomecoach111@heddoko.co",
                      Username = "awesomecoach111",
                      Status = UserStatusType.Active,
                      Password = pwd.Hash,
                      Salt = pwd.Salt,
                      FirstName = "Awesome",
                      LastName = "Coach",
                      Role = UserRoleType.Analyst
                  },
                  new User
                  {
                      Email = "footballdemo@heddoko.co",
                      Username = "footballdemo",
                      Status = UserStatusType.Active,
                      Password = pwd.Hash,
                      Salt = pwd.Salt,
                      FirstName = "Brian",
                      LastName = "Demo",
                      Role = UserRoleType.Analyst
                  },
                  new User
                  {
                      Email = "lzane@rogers.com",
                      Username = "lzane",
                      Status = UserStatusType.Active,
                      Password = pwd.Hash,
                      Salt = pwd.Salt,
                      FirstName = "Lzane",
                      LastName = "Demo",
                      Role = UserRoleType.Analyst
                  },
                  new User
                  {
                      Email = "license@heddoko.co",
                      Username = "license",
                      Status = UserStatusType.Active,
                      Password = pwd.Hash,
                      Salt = pwd.Salt,
                      FirstName = "License",
                      LastName = "Demo",
                      Role = UserRoleType.LicenseAdmin
                  }
              );
            context.SaveChanges();
        }

        private void Groups(HDContext context)
        {
            List<string> groups = new List<string>();
            groups.AddRange(new List<string>(){
                "Dummy Team",
                "Chargers",
                "Falcons",
                "The Stampeders",
                "Vikings",
                "Volleyball Team Supreme",
                "capop",
                "Olympians",
                "Heddoko",
                "Bulls",
                "Lions",
                "Oilers",
                "MTC"
            });

            context.Groups.AddOrUpdate(
                p => p.Name,
                groups.Select(c => new Group()
                {
                    Name = c,
                    Managers = new List<User>(){
                        GetUser("awesomecoach111@heddoko.co")
                    }
                }).ToArray()
            );

            context.Groups.AddOrUpdate(
              p => p.Name,
              new Group()
              {
                  Name = "Warriors",
                  AssetID = AddAsset(context, "/seed/war.png", AssetType.Seed),
                  Managers = new List<User>(){
                    GetUser("footballdemo@heddoko.co")
                  }
              },
              new Group()
              {
                  Name = "Lions",
                  AssetID = AddAsset(context, "/seed/lion.png", AssetType.Seed),
                  Managers = new List<User>(){
                    GetUser("footballdemo@heddoko.co")
                  }
              },
              new Group()
              {
                  Name = "Wolves",
                  AssetID = AddAsset(context, "/seed/wolf.png", AssetType.Seed),
                  Managers = new List<User>(){
                    GetUser("footballdemo@heddoko.co")
                  }
              },
              new Group()
              {
                  Name = "Bulls",
                  AssetID = AddAsset(context, "/seed/bull.png", AssetType.Seed),
                  Managers = new List<User>(){
                    GetUser("footballdemo@heddoko.co")
                  }
              }
            );

            context.Groups.AddOrUpdate(
              p => p.Name,
              new Group()
              {
                  Name = "Lions",
                  AssetID = AddAsset(context, "/seed/lion2.png", AssetType.Seed),
                  Managers = new List<User>(){
                    GetUser("lzane@rogers.com")
                  }
              }
            );
            context.SaveChanges();
        }

        private void Profiles(HDContext context)
        {
            context.Profiles.AddOrUpdate(
                p => p.FirstName,
                new Profile()
                {
                    FirstName = "Kara",
                    LastName = "Romanu",
                    Height = 1.63,
                    Email = "kara@example.com",
                    AssetID = AddAsset(context, "/seed/kara-dummy.jpg", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Dummy Team")
                    },
                    Managers = new List<User>(){
                        GetUser("awesomecoach111@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Mike",
                    LastName = "Watts",
                    Height = 1.88,
                    Email = "mike@example.com",
                    Groups = new List<Group>()
                    {
                        GetGroup("Dummy Team")
                    },
                    Managers = new List<User>(){
                        GetUser("awesomecoach111@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Svetlana",
                    LastName = "Vladsky",
                    Height = 1.75,
                    Email = "svetlana@example.com",
                    Gender = UserGenderType.Female,
                    Groups = new List<Group>()
                    {
                        GetGroup("Dummy Team")
                    },
                    Managers = new List<User>(){
                        GetUser("awesomecoach111@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Colin",
                    LastName = "Marechal",
                    Height = 1.93,
                    Weight = 104.33,
                    BirthDay = new DateTime(1987, 11, 3),
                    Gender = UserGenderType.Male,
                    Phone = "555-123-4321",
                    Email = "colin@example.com",
                    AssetID = AddAsset(context, "/seed/colin-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Jonathan",
                    LastName = "Jackson",
                    Height = 1.88,
                    Weight = 99.79,
                    BirthDay = null,
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = "jonathan@example.com",
                    AssetID = AddAsset(context, "/seed/john-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Peter",
                    LastName = "Groulx",
                    Height = 1.8,
                    Weight = 104.33,
                    BirthDay = new DateTime(1983, 4, 14),
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = "peter@example.com",
                    AssetID = AddAsset(context, "/seed/peter-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Babacar",
                    LastName = "Conte",
                    Height = 1.96,
                    Weight = 93.89,
                    BirthDay = new DateTime(1988, 9, 7),
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = null,
                    AssetID = AddAsset(context, "/seed/baba-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Petrov",
                    LastName = "Dimitru",
                    Height = 1.78,
                    Weight = 84.82,
                    BirthDay = new DateTime(1993, 7, 10),
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = null,
                    AssetID = AddAsset(context, "/seed/dima-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Jamal",
                    LastName = "Brown",
                    Height = 1.9,
                    Weight = 111.13,
                    BirthDay = new DateTime(1990, 3, 25),
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = null,
                    AssetID = AddAsset(context, "/seed/jamal-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Carl",
                    LastName = "Woods",
                    Height = 2.01,
                    Weight = 90.26,
                    BirthDay = new DateTime(1990, 5, 8),
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = "carl.woods@example.com",
                    AssetID = AddAsset(context, "/seed/carl-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "James",
                    LastName = "Rosling",
                    Height = 1.75,
                    Weight = 79.83,
                    BirthDay = new DateTime(1988, 8, 5),
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = null,
                    AssetID = AddAsset(context, "/seed/james-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                },
                new Profile()
                {
                    FirstName = "Kevin",
                    LastName = "Reese",
                    Height = 1.98,
                    Weight = 91.17,
                    BirthDay = new DateTime(1993, 1, 19),
                    Gender = UserGenderType.Male,
                    Phone = null,
                    Email = null,
                    AssetID = AddAsset(context, "/seed/kevin-war.png", AssetType.Seed),
                    Groups = new List<Group>()
                    {
                        GetGroup("Warriors")
                    },
                    Managers = new List<User>(){
                        GetUser("footballdemo@heddoko.co")
                    }
                }
            );
            context.SaveChanges();
        }

        private void Tags(HDContext context)
        {
            List<string> tags = new List<string>();
            tags.AddRange(new List<string>(){
                "American Football",
                "Cross Fit", "Curling",
                "Diving",
                "Figure Skating", "Football", "Golf",
                "Hockey",
                "Soccer", "Speed Skating", "Swimming", "Synchronized Swimming",
                "Tennis",
                "Weight Lifting",

                // General movements.
                "Back Squat", "Behind the Neck Press", "Behind The Neck Push Jerk",
                "Bench Fly", "Bench Press",
                "Bent Over Row", "Bicep Curl",
                "Box Squat", "Bulgarian Squat",
                "Clean", "Clean Off the Blocks", "Counter Movement Jump",
                "Deadlift", "Decline Bench Press", "Dribble",
                "Front Raise", "Front Squat",
                "Goblet Squat",
                "Half Squat",
                "Hang Clean", "Hang Power Clean", "Hang Power Snatch", "Hang Snatch", "Hip Thrust",
                "Incline Bench Fly", "Incline Bench Press",
                "Jump Squat",
                "Kettlebell Swing",
                "Lateral Raise", "Lunge",
                "Medicine Ball Slam",
                "One Arm Snatch", "Overhead Press", "Overhead Squat",
                "Power Clean", "Power Clean Off the Blocks", "Power Snatch", "Power Snatch Off the Blocks",
                "Push Jerk", "Push Press",
                "Reverse Fly", "Romanian Deadlift",
                "Seated Military Press",
                "Snatch", "Snatch Off the Blocks", "Split Jerk", "Split Squat",
                "Squat Jump", "Standing Military Press", "Step-ups", "Stiff-legged Deadlift", "Sumo Deadlift",
                "Trap Bar Deadlift", "Travelling Lunges",
                "Wide Grip Behind the Neck Push Jerk", "Wide Grip Deadlift",

                // Additional tags.
                "Barbell",
                "Dumbbell",
                "Narrow Grip",
                "Wide Grip"
            });


            context.Tags.AddOrUpdate(
                p => p.Title,
                tags.Select(c => new Tag()
                {
                    Title = c
                }).ToArray()
            );
            context.SaveChanges();
        }

        private void Suits(HDContext context)
        {
            context.MaterialTypes.AddOrUpdate(
                p => p.Identifier,
                new MaterialType()
                {
                    Identifier = "Battery"
                }, new MaterialType()
                {
                    Identifier = "Sensor"
                }
            );
            context.SaveChanges();

            context.Equipments.RemoveRange(context.Equipments.ToList());
            context.ComplexEquipments.RemoveRange(context.ComplexEquipments.ToList());
            context.SaveChanges();

            context.Materials.RemoveRange(context.Materials.ToList());
            context.SaveChanges();

            context.Materials.AddOrUpdate(
                p => p.Name,
                new Material()
                {
                    Name = "Sample Nod",
                    PartNo = "12345",
                    MaterialTypeID = GetMaterialType("Sensor").ID
                },
                new Material()
                {
                    Name = "Sample StretchSense sensor",
                    PartNo = "54334",
                    MaterialTypeID = GetMaterialType("Sensor").ID
                },
                new Material()
                {
                    Name = "Sample Battery Pack",
                    PartNo = "32432",
                    MaterialTypeID = GetMaterialType("Battery").ID
                }
            );
            context.SaveChanges();

            for (int i = 1; i <= 10; i++)
            {
                context.ComplexEquipments.AddOrUpdate(
                    p => p.MacAddress,
                    new ComplexEquipment()
                    {
                        MacAddress = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                        SerialNo = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                        PhysicalLocation = "Warehouse",
                        Status = EquipmentStatusType.Unavailable,
                        Equipments = new List<Equipment>()
                        {
                            new Equipment()
                            {
                                MacAddress = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                                SerialNo = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                                PhysicalLocation = "Box 2",
                                Status = EquipmentStatusType.Unavailable,
                                AnatomicalPosition = AnatomicalPositionType.LeftTibia,
                                Condition = ConditionType.New,
                                HeatsShrink = HeatsShrinkType.No,
                                Material = GetMaterial("Sample Nod"),
                                Numbers = NumbersType.No,
                                Prototype = PrototypeType.Yes,
                                Ship = ShipType.No,
                                VerifiedBy = GetUser("support@heddoko.co")
                            },
                            new Equipment()
                            {
                                MacAddress = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                                SerialNo = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                                PhysicalLocation = "Box 2",
                                Status = EquipmentStatusType.Unavailable,
                                AnatomicalPosition = AnatomicalPositionType.RightForeArm,
                                Condition = ConditionType.Used,
                                HeatsShrink = HeatsShrinkType.No,
                                Material = GetMaterial("Sample StretchSense sensor"),
                                Numbers = NumbersType.Yes,
                                Prototype = PrototypeType.Yes,
                                Ship = ShipType.Gone,
                                VerifiedBy = GetUser("support@heddoko.co")
                            },
                            new Equipment()
                            {
                                MacAddress = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                                SerialNo = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                                PhysicalLocation = "Box 2",
                                Status = EquipmentStatusType.Unavailable,
                                AnatomicalPosition = null,
                                Condition = ConditionType.New,
                                HeatsShrink = HeatsShrinkType.No,
                                Material = GetMaterial("Sample Battery Pack"),
                                Numbers = NumbersType.Yes,
                                Prototype = PrototypeType.Yes,
                                Ship = ShipType.No,
                                VerifiedBy = GetUser("support@heddoko.co")
                            }
                        }
                    }
                );

                context.Equipments.AddOrUpdate(
                p => p.MacAddress,
                    new Equipment()
                    {
                        MacAddress = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                        SerialNo = PasswordHasher.GenerateRandomSalt(6).Replace("==", ""),
                        PhysicalLocation = "Box 1",
                        Status = EquipmentStatusType.Available,
                        AnatomicalPosition = null,
                        Condition = ConditionType.Used,
                        HeatsShrink = HeatsShrinkType.No,
                        Material = GetMaterial("Sample StretchSense sensor"),
                        Numbers = NumbersType.Yes,
                        Prototype = PrototypeType.Yes,
                        Ship = ShipType.No,
                        VerifiedBy = GetUser("support@heddoko.co")
                    }
                );
            }
        }
    }
}
