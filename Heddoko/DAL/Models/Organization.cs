using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class Organization : BaseModel, IAuditable, ISoftDelete
    {
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        public string Notes { get; set; }

        public OrganizationStatusType Status { get; set; }
        #region NotMapped

        public string IDView => $"OR{Id.ToString(Constants.PadZero)}";

        bool ISoftDelete.IsDeleted => Status == OrganizationStatusType.Deleted;
        #endregion

        #region Relations

        [JsonIgnore]
        public int UserID { get; set; }

        [JsonIgnore]
        [ForeignKey("UserID")]
        [JilDirective(Ignore = true)]
        public virtual User User { get; set; }

        [JilDirective(Ignore = true)]
        public virtual ICollection<User> Users { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        public virtual ICollection<License> Licenses { get; set; }


        [JsonIgnore]
        public virtual ICollection<Kit> Kits { get; set; }


        #endregion
    }
}