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

        [JsonIgnore]
        public DateTime? ForgotExpiration { get; set; }

        #region Relations
        [JsonIgnore]
        public int? AssetID { get; set; }

        [JsonIgnore]
        public virtual Asset Asset { get; set; }

        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profile> Profiles { get; set; }

        [JsonIgnore]
        public virtual ICollection<AccessToken> Tokens { get; set; }

        [JsonIgnore]
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
