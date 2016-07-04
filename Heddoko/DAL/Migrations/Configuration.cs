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
                Users(context);
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
                   }
              );
            context.SaveChanges();
        }
    }
}
