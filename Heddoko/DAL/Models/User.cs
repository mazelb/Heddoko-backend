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
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IBaseModel
    {
        private string _roleName;

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager, string authenticationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            if (string.IsNullOrEmpty(SecurityStamp))
            {
                SecurityStamp = Guid.NewGuid().ToString();
            }
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            if (ParentLoggedInUserId.HasValue)
            {
                userIdentity.AddClaim(new Claim(Constants.ClaimTypes.ParentLoggedInUser, ParentLoggedInUserId.ToString()));
            }

            return userIdentity;
        }

        #region BaseModel
        [JsonProperty("updatedAt")]
        public DateTime? Updated { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonProperty("createdAt")]
        public DateTime Created { get; set; }
        #endregion

        #region IdentityUser
        [JsonIgnore]
        public override int AccessFailedCount { get; set; }
        public override string Email { get; set; }
        [JsonIgnore]
        public override bool EmailConfirmed { get; set; }
        public override int Id { get; set; }
        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }
        [JsonIgnore]
        public override DateTime? LockoutEndDateUtc { get; set; }
        [JsonIgnore]
        public override string PasswordHash { get; set; }
        public override string PhoneNumber { get; set; }
        [Obsolete("will be removed after migration to Identity")]
        public string Phone => PhoneNumber;
        [JsonIgnore]
        public override bool PhoneNumberConfirmed { get; set; }
        [JsonIgnore]
        public override string SecurityStamp { get; set; }
        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }
        public override string UserName { get; set; }
        #endregion

        [StringLength(100)]
        [Obsolete("will be removed after migration to Identity")]
        [JsonIgnore]
        public string Password { get; set; }

        [StringLength(100)]
        [Obsolete("will be removed after migration to Identity")]
        [JsonIgnore]
        public string Salt { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(20)]
        public string Country { get; set; }

        public DateTime? BirthDay { get; set; }

        [JsonIgnore]
        [Obsolete("will be removed after migration to Identity")]
        public UserRoleType Role { get; set; }

        public UserStatusType Status { get; set; }

        #region Relations

        [JsonIgnore]
        public int? TeamID { get; set; }

        public virtual Team Team { get; set; }

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
        public virtual ICollection<Kit> Kits { get; set; }

        [JilDirective(Ignore = true)]
        public virtual Kit Kit => Kits?.FirstOrDefault();

        [JsonIgnore]
        public virtual ICollection<Device> Devices { get; set; }
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
                ID = License.Id,
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
                if (License == null ||
                    !License.IsActive)
                {
                    return UserRoleType.User;
                }

                switch (License.Type)
                {
                    case LicenseType.DataAnalysis:
                        return UserRoleType.Analyst;
                    case LicenseType.DataCollection:
                        return UserRoleType.Worker;
                    case LicenseType.Universal:
                        return UserRoleType.LicenseUniversal;
                    default:
                        return UserRoleType.User;
                }
            }
        }

        [JsonIgnore]
        public bool IsActive => Status == UserStatusType.Active;

        [JsonIgnore]
        public bool IsBanned => Status == UserStatusType.Banned;
        [JsonIgnore]
        public bool IsNotApproved => Status == UserStatusType.Pending;

        [NotMapped]
        [JsonIgnore]
        public string RoleName
        {
            get
            {
                return _roleName ??
                       (_roleName = Roles.FirstOrDefault(r => r.Role?.Name != Constants.Roles.User)?.Role.Name ?? Constants.Roles.User);
            }
            set { _roleName = value; }
        }

        [NotMapped]
        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public int? ParentLoggedInUserId { get; set; }

        #endregion
    }
}