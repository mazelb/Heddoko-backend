using DAL.Helpers;
using Jil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : BaseModel
    {
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Email { get; set; }

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

        public UserRoleType RoleType
        {
            get
            {
                if (License != null
                && License.IsActive)
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

        public string LicenseInfoToken
        {
            get
            {
                if (License != null)
                {
                    LicenseInfo info = new LicenseInfo()
                    {
                        ID = License.ID,
                        ExpiredAt = License.ExpirationAt,
                        Name = License.Name,
                        Status = License.Status,
                        Type = License.Type,
                        ViewID = License.ViewID,
                        IsActive = License.IsActive
                    };

                    string json = JsonConvert.SerializeObject(info);

                    return JWTHelper.Create(json);
                }

                return null;
            }
        }

        [JsonIgnore]
        public int? AssetID { get; set; }

        [JsonIgnore]
        public virtual Asset Asset { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Group> Groups { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Profile> Profiles { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<AccessToken> Tokens { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<Equipment> Equipments { get; set; }
        #endregion

        #region NotMapped
        public bool AllowToken()
        {
            if (Tokens != null
             && Tokens.Count() > 0)
            {
                Token = Tokens.FirstOrDefault()?.Token;
                return true;
            }

            return false;
        }

        public string GenerateToken()
        {
            return PasswordHasher.Md5(Email + DateTime.UtcNow.Ticks.ToString());
        }

        public string Name
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public string AvatarSrc
        {
            get
            {
                return Asset == null ? string.Empty : Asset.Url;
            }
        }

        [NotMapped]
        public string Token { get; set; }

        [JsonIgnore]
        public List<string> Roles
        {
            get
            {
                return new List<string>() { Role.GetStringValue() };
            }
        }

        [JsonIgnore]
        public bool IsActive
        {
            get
            {
                return Status == UserStatusType.Active;
            }
        }

        [JsonIgnore]
        public bool IsAdmin
        {
            get
            {
                return Role == UserRoleType.Admin;
            }
        }

        [JsonIgnore]
        public bool IsBanned
        {
            get
            {
                return Status == UserStatusType.Banned;
            }
        }
        #endregion
    }
}
