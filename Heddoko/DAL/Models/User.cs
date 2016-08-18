using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Helpers;
using Jil;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<User, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(HDContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(HDContext context)
            : base(context)
        {
        }
    }

    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IBaseModel
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        #region BaseModel
        [NotMapped]
        public int ID
        {
            get { return Id; }
            set { Id = value; }
        }

        [JsonProperty("updatedAt")]
        public DateTime? Updated { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonProperty("createdAt")]
        public DateTime Created { get; set; }
        #endregion

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Username { get; set; }

        [StringLength(100)]
        [JsonIgnore]
        public string Password { get; set; }

        [StringLength(100)]
        [JsonIgnore]
        public string Salt { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        public DateTime? BirthDay { get; set; }

        [JsonIgnore]
        public UserRoleType Role { get; set; }

        public UserStatusType Status { get; set; }

        [StringLength(100)]
        [JsonIgnore]
        public string ConfirmToken { get; set; }

        [StringLength(100)]
        [JsonIgnore]
        public string RememberToken { get; set; }

        [StringLength(100)]
        [JsonIgnore]
        public string ForgotToken { get; set; }

        [StringLength(100)]
        [JsonIgnore]
        public string InviteToken { get; set; }

        [JsonIgnore]
        public DateTime? ForgotExpiration { get; set; }

        #region Relations

        [JsonIgnore]
        public int? OrganizationID { get; set; }

        [JsonIgnore]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        public int? LicenseID { get; set; }

        [JsonIgnore]
        public virtual License License { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<AccessToken> Tokens { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Asset> Assets { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Kit> Kits { get; set; }

        public virtual Kit Kit => Kits?.FirstOrDefault();
        #endregion

        #region NotMapped
        public bool AllowLicenseInfoToken()
        {
            if (License == null)
            {
                return false;
            }

            LicenseInfo info = new LicenseInfo
            {
                ID = License.ID,
                ExpiredAt = License.ExpirationAt,
                Name = License.Name,
                Status = License.Status,
                Type = License.Type,
                ViewID = License.ViewID,
                IDView = License.IDView
            };

            string json = JsonConvert.SerializeObject(info);

            LicenseInfoToken = JwtHelper.Create(json);

            return true;
        }


        public bool AllowToken()
        {
            if (Tokens == null ||
                !Tokens.Any())
            {
                return false;
            }

            Token = Tokens.FirstOrDefault()?.Token;

            return true;
        }

        public string GenerateToken()
        {
            return PasswordHasher.Md5(Email + DateTime.UtcNow.Ticks);
        }

        public string Name => $"{FirstName} {LastName}";

        [NotMapped]
        [JilDirective(Ignore = true)]
        public string Token { get; set; }

        [NotMapped]
        [JilDirective(Ignore = true)]
        public string LicenseInfoToken { get; set; }

        public UserRoleType RoleType
        {
            get
            {
                if (License != null
                    &&
                    License.IsActive)
                {
                    switch (License.Type)
                    {
                        case LicenseType.DataAnalysis:
                            return UserRoleType.Analyst;
                        case LicenseType.DataCollection:
                            return UserRoleType.Worker;
                    }
                }
                return Role;
            }
        }

        [JsonIgnore]
        public bool IsActive => Status == UserStatusType.Active;

        [JsonIgnore]
        public bool IsAdmin => Role == UserRoleType.Admin;

        [JsonIgnore]
        public bool IsBanned => Status == UserStatusType.Banned;
        #endregion
    }
}