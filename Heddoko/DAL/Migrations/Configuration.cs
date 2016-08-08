using System.Data.Entity.Migrations;
using DAL.Models;

namespace DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<HDContext>
    {
        public Configuration()
        {
            //TODO Disable it
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            CommandTimeout = 360000;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(HDContext context)
        {
            if (!Config.AllowInitData)
            {
                return;
            }

            Users(context);
        }

        private static void Users(HDContext context)
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