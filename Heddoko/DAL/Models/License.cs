using System;
using System.Collections.Generic;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class License : BaseModel
    {
        public LicenseType Type { get; set; }

        public int Amount { get; set; }

        public LicenseStatusType Status { get; set; }

        public DateTime ExpirationAt { get; set; }

        #region Relation

        [JsonIgnore]
        public int? OrganizationID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<User> Users { get; set; }

        #endregion

        #region NotMapped

        public string IDView => $"LI{ID.ToString(Constants.PadZero)}";

        public string ViewID => $"{OrganizationID}-{ID}";


        public string Name => $"{Type.GetDisplayName()} {IDView} ({ExpirationAt.ToString("dd/MM/yyyy")})";

        public bool IsActive => (Type == LicenseType.DataAnalysis || Type == LicenseType.DataCollection)
                                && Status == LicenseStatusType.Active
                                && ExpirationAt >= DateTime.Now;

        //TODO remove that later
        public bool Validate()
        {
            if (ExpirationAt > DateTime.Now)
            {
                if (Status == LicenseStatusType.Expired)
                {
                    Status = LicenseStatusType.Active;
                    return true;
                }
                return false;
            }

            if (Status != LicenseStatusType.Active
                &&
                Status != LicenseStatusType.Inactive)
            {
                return false;
            }

            Status = LicenseStatusType.Expired;

            return true;
        }

        #endregion
    }
}