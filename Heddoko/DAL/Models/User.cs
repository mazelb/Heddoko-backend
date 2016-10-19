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
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            SecurityStamp = Guid.NewGuid().ToString();
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }

        #region BaseModel
        [JsonProperty("updatedAt")]
        public DateTime? Updated { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonProperty("createdAt")]
        public DateTime Created { get; set; }
        #endregion

        [StringLength(100)]
        [Obsolete("will be removed after migration to Identity")]
        public string Password { get; set; }

        [StringLength(100)]
        [Obsolete("will be removed after migration to Identity")]
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
        public bool IsBanned => Status == UserStatusType.Banned;
        [JsonIgnore]
        public bool IsNotApproved => Status == UserStatusType.Pending;

        #endregion
    }
}