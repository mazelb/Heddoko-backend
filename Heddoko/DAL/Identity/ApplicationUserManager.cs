using DAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationUserManager : UserManager<User, int>
    {
        private const string InviteToken = "Invite";

        /// <summary>
        /// ResetPassword is a private key in UserManager class. In case of error check if they still use this key for reseting pwd.
        /// </summary>
        private const string ResetPassword = "ResetPassword";

        public ApplicationUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }

        public UnitOfWork UoW { get; set; }

        public UserStore UserStore => Store as UserStore;

        public EmailService UserEmailService => EmailService as EmailService;

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore(context.Get<UnitOfWork>().Context))
            {
                UoW = context.Get<UnitOfWork>()
            };

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
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromDays(5)
                };
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

                    if (!await IsInRoleAsync(user.Id, Constants.Roles.User))
                    {
                        await AddToRoleAsync(user.Id, Constants.Roles.User);
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

        public async Task<string> GenerateInviteTokenAsync(User user)
        {
            return await UserTokenProvider.GenerateAsync(InviteToken, this, user);
        }

        public string GenerateInviteToken(int userId)
        {
            return this.GenerateUserToken(InviteToken, userId);
        }

        public async Task<bool> ValidateInviteTokenAsync(User user, string token)
        {
            return await UserTokenProvider.ValidateAsync(InviteToken, token, this, user);
        }

        public async Task<bool> ValidateResetPasswordToken(User user, string token)
        {
            return await UserTokenProvider.ValidateAsync(ResetPassword, token, this, user);
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

        public User FindByToken(string token)
        {
            return UserStore.FindByToken(token);
        }

        public void SendActivationEmail(int userId, string code)
        {
            UserEmailService.Service.SendActivationEmail(userId, code);
        }

        public void SendInviteAdminEmail(int organizationId, string inviteToken)
        {
            UserEmailService.Service.SendInviteAdminEmail(organizationId, inviteToken);
        }

        public void SendInviteEmail(int userId, string inviteToken)
        {
            UserEmailService.Service.SendInviteEmail(userId, inviteToken);
        }

        public void SendForgotPasswordEmail(int userId, string resetPasswordToken)
        {
            UserEmailService.Service.SendForgotPasswordEmail(userId, resetPasswordToken);
        }

        public void SendForgotUsernameEmail(int userId)
        {
            UserEmailService.Service.SendForgotUsernameEmail(userId);
        }

        public void SendSupportEmail(ISupportEmailViewModel model)
        {
            UserEmailService.Service.SendSupportEmail(model);
        }

        public void SendActivatedEmail(int userId)
        {
            UserEmailService.Service.SendActivatedEmail(userId);
        }

        public void ApplyUserRolesForLicense(User user)
        {
            if (user.License == null)
            {
                this.RemoveFromRoles(user.Id, Constants.Roles.Analyst, Constants.Roles.Worker);
            }
            else
            {
                if (!this.IsInRole(user.Id, Constants.Roles.Admin))
                {
                    switch (user.License.Type)
                    {
                        case LicenseType.DataAnalysis:
                            this.AddToRole(user.Id, Constants.Roles.Analyst);
                            break;
                        case LicenseType.DataCollection:
                            this.AddToRole(user.Id, Constants.Roles.Worker);
                            break;
                    }
                }
            }
        }

        public async Task<IdentityResult> SetEmailConfirmedAsync(User user, bool confirmed)
        {
            await UserStore.SetEmailConfirmedAsync(user, confirmed);

            return await UpdateAsync(user);
        }
    }
}
