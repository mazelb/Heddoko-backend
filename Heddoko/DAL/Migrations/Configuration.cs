using System.Data.Entity.Migrations;
using System.Linq;
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
            Passphrase pwd = PasswordHasher.Hash("H3dd0k0_1323$");
            Passphrase pwd2 = PasswordHasher.Hash("H3dd0k0_4242$");
            Passphrase pwd3 = PasswordHasher.Hash("H3dd0k0_anuk34$");


            //if (context.Users.Any())
            //{
            //    return;
            //}

            context.Users.AddOrUpdate(
                p => p.Email,
                new User
                {
                    Email = "ss@a2a.co",
                    UserName = "ss",
                    Status = UserStatusType.Active,
                    FirstName = "S",
                    LastName = "S",
                    Role = UserRoleType.Admin
                },
                new User
                {
                    Email = "admin@heddoko.com",
                    UserName = "heddoko.admin",
                    Status = UserStatusType.Active,
                    FirstName = "Admin",
                    LastName = "",
                    Role = UserRoleType.Admin
                },
                new User
                {
                    Email = "support@heddoko.com",
                    UserName = "heddoko.support",
                    Status = UserStatusType.Active,
                    FirstName = "Support",
                    LastName = "",
                    Role = UserRoleType.Admin
                },
                new User
                {
                    Email = "ankit@heddoko.com",
                    UserName = "ankit.heddoko",
                    Status = UserStatusType.Active,
                    FirstName = "Ankit",
                    LastName = "Vasu",
                    Role = UserRoleType.Admin
                }
                );
            context.SaveChanges();
        }
    }
}