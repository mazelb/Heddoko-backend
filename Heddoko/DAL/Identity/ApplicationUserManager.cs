﻿using DAL;
using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public UnitOfWork UoW { get; set; }

        public UserStore UserStore
        {
            get
            {
                return Store as UserStore;
            }
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore(context.Get<UnitOfWork>().Context));
            manager.UoW = context.Get<UnitOfWork>();

            manager.UserValidator = new UserValidator<User, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            manager.EmailService = context.Get<EmailService>();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        public override async Task<bool> CheckPasswordAsync(User user, string password)
        {
            //TODO Remove that after migration all users. can be done only while Sign In step
            if (!string.IsNullOrEmpty(user.Salt))
            {
                if (DAL.PasswordHasher.Equals(password?.Trim(), user.Salt, user.Password))
                {
                    string newPasswordHash = PasswordHasher.HashPassword(password?.Trim());

                    IUserPasswordStore<User, int> store = Store as IUserPasswordStore<User, int>;
                    await store.SetPasswordHashAsync(user, newPasswordHash);

                    user.Salt = null;
                    user.Password = null;

                    if (user.Status != UserStatusType.Invited
                     && user.Status != UserStatusType.Pending)
                    {
                        user.EmailConfirmed = true;
                    }

                    await UpdateAsync(user);

                    switch (user.Role)
                    {
                        case UserRoleType.Admin:
                            if (!await IsInRoleAsync(user.Id, Constants.Roles.Admin))
                            {
                                await AddToRoleAsync(user.Id, Constants.Roles.Admin);
                            }
                            break;
                        case UserRoleType.LicenseAdmin:
                            if (!await IsInRoleAsync(user.Id, Constants.Roles.LicenseAdmin))
                            {
                                await AddToRoleAsync(user.Id, Constants.Roles.LicenseAdmin);
                            }
                            break;
                        case UserRoleType.User:
                            if (!await IsInRoleAsync(user.Id, Constants.Roles.LicenseAdmin))
                            {
                                await AddToRoleAsync(user.Id, Constants.Roles.User);
                            }
                            break;
                        default:
                            break;
                    }

                    switch (user.RoleType)
                    {
                        case UserRoleType.Worker:
                            if (!await IsInRoleAsync(user.Id, Constants.Roles.Worker))
                            {
                                await AddToRoleAsync(user.Id, Constants.Roles.Worker);
                            }
                            break;
                        case UserRoleType.Analyst:
                            if (!await IsInRoleAsync(user.Id, Constants.Roles.Analyst))
                            {
                                await AddToRoleAsync(user.Id, Constants.Roles.Analyst);
                            }
                            break;
                        default:
                            break;
                    }

                    UoW.UserRepository.ClearCache(user);

                    return true;
                }
            }

            return await base.CheckPasswordAsync(user, password);
        }

        public User FindByIdCached(int userId)
        {
            return UserStore.FindByIdCached(userId);
        }

        public User FindByNameCached(string userName)
        {
            return UserStore.FindByNameCached(userName);
        }

        public User FindByEmailCached(string email)
        {
            return UserStore.FindByEmailCached(email);
        }
    }
}
