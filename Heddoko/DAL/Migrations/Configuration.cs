using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

            Roles(context);
            Users(context);
        }

        private static void Users(HDContext context)
        {
            string pwd = "H3dd0k0_1323$";
            string pwd2 = "H3dd0k0_4242$";
            string pwd3 = "H3dd0k0_anuk34$";
            Microsoft.AspNet.Identity.PasswordHasher PasswordHash = new Microsoft.AspNet.Identity.PasswordHasher();

            //if (context.Users.Any())
            //{
            //    return;
            //}

            ApplicationUserManager manager = new ApplicationUserManager(new UserStore(context));

            User user = manager.FindByName("ss") ?? new User();

            user.Email = "ss@a2a.co";
            user.UserName = "ss";
            user.Status = UserStatusType.Active;
            user.FirstName = "S";
            user.LastName = "S";
            user.EmailConfirmed = true;
            user.Salt = null;
            user.Password = null;
            user.Role = UserRoleType.Admin;
            user.SecurityStamp = Guid.NewGuid().ToString();

            if (user.Id == 0)
            {
                manager.Create(user, pwd);
                manager.AddToRole(user.Id, Constants.Roles.User);
                manager.AddToRole(user.Id, Constants.Roles.Admin);
            }
            else
            {
                user.PasswordHash = PasswordHash.HashPassword(pwd);
                manager.Update(user);

                if (!manager.IsInRole(user.Id, Constants.Roles.User))
                {
                    manager.AddToRole(user.Id, Constants.Roles.User);
                }

                if (!manager.IsInRole(user.Id, Constants.Roles.Admin))
                {
                    manager.AddToRole(user.Id, Constants.Roles.Admin);
                }
            }

            user = manager.FindByName("heddoko.admin") ?? new User();

            user.Email = "admin@heddoko.com";
            user.UserName = "heddoko.admin";
            user.Status = UserStatusType.Active;
            user.FirstName = "Admin";
            user.LastName = "";
            user.EmailConfirmed = true;
            user.Salt = null;
            user.Password = null;
            user.Role = UserRoleType.Admin;
            user.SecurityStamp = Guid.NewGuid().ToString();

            if (user.Id == 0)
            {
                manager.Create(user, pwd);
                manager.AddToRole(user.Id, Constants.Roles.User);
                manager.AddToRole(user.Id, Constants.Roles.Admin);
            }
            else
            {
                user.PasswordHash = PasswordHash.HashPassword(pwd);
                manager.Update(user);

                if (!manager.IsInRole(user.Id, Constants.Roles.User))
                {
                    manager.AddToRole(user.Id, Constants.Roles.User);
                }

                if (!manager.IsInRole(user.Id, Constants.Roles.Admin))
                {
                    manager.AddToRole(user.Id, Constants.Roles.Admin);
                }
            }

            user = manager.FindByName("heddoko.support") ?? new User();

            user.Email = "support@heddoko.com";
            user.UserName = "heddoko.support";
            user.Status = UserStatusType.Active;
            user.EmailConfirmed = true;
            user.FirstName = "Support";
            user.LastName = "";
            user.Salt = null;
            user.Password = null;
            user.Role = UserRoleType.Admin;
            user.SecurityStamp = Guid.NewGuid().ToString();

            if (user.Id == 0)
            {
                manager.Create(user, pwd2);
                manager.AddToRole(user.Id, Constants.Roles.User);
                manager.AddToRole(user.Id, Constants.Roles.Admin);
            }
            else
            {
                user.PasswordHash = PasswordHash.HashPassword(pwd2);
                manager.Update(user);

                if (!manager.IsInRole(user.Id, Constants.Roles.User))
                {
                    manager.AddToRole(user.Id, Constants.Roles.User);
                }

                if (!manager.IsInRole(user.Id, Constants.Roles.Admin))
                {
                    manager.AddToRole(user.Id, Constants.Roles.Admin);
                }
            }

            user = manager.FindByName("ankit.heddoko") ?? new User();

            user.Email = "ankit@heddoko.com";
            user.UserName = "ankit.heddoko";
            user.Status = UserStatusType.Active;
            user.EmailConfirmed = true;
            user.FirstName = "Ankit";
            user.LastName = "Vasu";
            user.Salt = null;
            user.Password = null;
            user.Role = UserRoleType.Admin;
            user.SecurityStamp = Guid.NewGuid().ToString();

            if (user.Id == 0)
            {
                manager.Create(user, pwd3);
                manager.AddToRole(user.Id, Constants.Roles.User);
                manager.AddToRole(user.Id, Constants.Roles.Admin);
            }
            else
            {
                user.PasswordHash = PasswordHash.HashPassword(pwd3);
                manager.Update(user);
                if (!manager.IsInRole(user.Id, Constants.Roles.User))
                {
                    manager.AddToRole(user.Id, Constants.Roles.User);
                }

                if (!manager.IsInRole(user.Id, Constants.Roles.Admin))
                {
                    manager.AddToRole(user.Id, Constants.Roles.Admin);
                }
            }

            user = manager.FindByName("service.admin") ?? new User();

            user.Email = "service.admin@heddoko.com";
            user.UserName = "service.admin";
            user.Status = UserStatusType.Active;
            user.EmailConfirmed = true;
            user.FirstName = "Service";
            user.LastName = "Admin";
            user.Salt = null;
            user.Password = null;
            user.Role = UserRoleType.ServiceAdmin;
            user.SecurityStamp = Guid.NewGuid().ToString();

            if (user.Id == 0)
            {
                manager.Create(user, pwd);
                manager.AddToRole(user.Id, Constants.Roles.User);
                manager.AddToRole(user.Id, Constants.Roles.ServiceAdmin);
            }
            else
            {
                user.PasswordHash = PasswordHash.HashPassword(pwd);
                manager.Update(user);
                if (!manager.IsInRole(user.Id, Constants.Roles.User))
                {
                    manager.AddToRole(user.Id, Constants.Roles.User);
                }

                if (!manager.IsInRole(user.Id, Constants.Roles.ServiceAdmin))
                {
                    manager.AddToRole(user.Id, Constants.Roles.ServiceAdmin);
                }
            }

            CreateLicenseUniversal(manager, new UnitOfWork(context));
        }

        private static void CreateLicenseUniversal(ApplicationUserManager manager, UnitOfWork unitOfWork)
        {
            string pwd = "H3dd0k0_universal$";
            User user = manager.FindByName("heddoko.universal") ?? new User();

            user.Email = "heddoko.universal@heddoko.com";
            user.UserName = "heddoko.universal";
            user.Status = UserStatusType.Active;
            user.EmailConfirmed = true;
            user.FirstName = "License";
            user.LastName = "Universal";
            user.Salt = null;
            user.Password = null;
            user.Role = UserRoleType.User;
            user.SecurityStamp = Guid.NewGuid().ToString();

            if (user.Id == 0)
            {
                manager.Create(user, pwd);
                manager.AddToRole(user.Id, Constants.Roles.User);
            }
            else
            {
                manager.Update(user);
                if (!manager.IsInRole(user.Id, Constants.Roles.User))
                {
                    manager.AddToRole(user.Id, Constants.Roles.User);
                }
            }

            var now = DateTime.Now;
            
            var license = user.License ?? new License { Created = now };

            license.Amount = 1;
            license.Created = now;
            license.ExpirationAt = new DateTime(2020, now.Month, now.Day).EndOfDay();
            license.Status = LicenseStatusType.Active;
            license.Type = LicenseType.Universal;

            if (license.Id == 0)
            {
                unitOfWork.LicenseRepository.Create(license);
                user.License = license;
            }

            user.Role = UserRoleType.LicenseUniversal;

            unitOfWork.Save();

            if (!manager.IsInRole(user.Id, Constants.Roles.LicenseUniversal))
            {
                manager.AddToRole(user.Id, Constants.Roles.LicenseUniversal);
            }
        }

        private static void Roles(HDContext context)
        {
            var roleManager = new RoleManager<Models.IdentityRole, int>(new RoleStore(context));

            if (!roleManager.RoleExists(Constants.Roles.Admin))
            {
                Models.IdentityRole role = new Models.IdentityRole()
                {
                    Name = Constants.Roles.Admin
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constants.Roles.Analyst))
            {
                Models.IdentityRole role = new Models.IdentityRole()
                {
                    Name = Constants.Roles.Analyst
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constants.Roles.User))
            {
                Models.IdentityRole role = new Models.IdentityRole()
                {
                    Name = Constants.Roles.User
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constants.Roles.LicenseAdmin))
            {
                Models.IdentityRole role = new Models.IdentityRole()
                {
                    Name = Constants.Roles.LicenseAdmin
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constants.Roles.Worker))
            {
                Models.IdentityRole role = new Models.IdentityRole()
                {
                    Name = Constants.Roles.Worker
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constants.Roles.ServiceAdmin))
            {
                Models.IdentityRole role = new Models.IdentityRole()
                {
                    Name = Constants.Roles.ServiceAdmin
                };
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists(Constants.Roles.LicenseUniversal))
            {
                Models.IdentityRole role = new Models.IdentityRole
                {
                    Name = Constants.Roles.LicenseUniversal
                };
                roleManager.Create(role);
            }
        }
    }
}