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

            //context.Users.AddOrUpdate(
            //    p => p.Username,
            //    new User
            //    {
            //        Email = "ss@a2a.co",
            //        Username = "ss",
            //        Status = UserStatusType.Active,
            //        Password = pwd.Hash,
            //        Salt = pwd.Salt,
            //        FirstName = "S2",
            //        LastName = "S",
            //        Role = UserRoleType.Admin
            //    },
            //    new User
            //    {
            //        Email = "admin@heddoko.com",
            //        Username = "heddoko.admin",
            //        Status = UserStatusType.Active,
            //        Password = pwd.Hash,
            //        Salt = pwd.Salt,
            //        FirstName = "Admin",
            //        LastName = "",
            //        Role = UserRoleType.Admin
            //    },
            //    new User
            //    {
            //        Email = "support@heddoko.com",
            //        Username = "heddoko.support",
            //        Status = UserStatusType.Active,
            //        Password = pwd2.Hash,
            //        Salt = pwd2.Salt,
            //        FirstName = "Support",
            //        LastName = "",
            //        Role = UserRoleType.Admin
            //    },
            //    new User
            //    {
            //        Email = "ankit@heddoko.com",
            //        Username = "ankit.heddoko",
            //        Status = UserStatusType.Active,
            //        Password = pwd3.Hash,
            //        Salt = pwd3.Salt,
            //        FirstName = "Ankit",
            //        LastName = "Vasu",
            //        Role = UserRoleType.Admin
            //    }
            //    );
            //context.SaveChanges();
        }
    }
}