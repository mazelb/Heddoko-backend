using Jil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class License : BaseModel
    {
        public string ViewID
        {
            get
            {
                return $"{OrganizationID}-{ID}";
            }
        }

        public string Name
        {
            get
            {
                return $"{Type.GetDisplayName()} ({ExpirationAt.ToString("dd/MM/yyyy")})";
            }
        }

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
        public bool IsActive
        {
            get
            {
                return (Type == LicenseType.DataAnalysis || Type == LicenseType.DataCollection)
                    && Status == LicenseStatusType.Active
                    && ExpirationAt > DateTime.Now;
            }
        }
        #endregion
    }
}
