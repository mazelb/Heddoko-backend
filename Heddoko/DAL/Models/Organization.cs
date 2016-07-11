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
    public class Organization : BaseModel
    {
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Phone { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        public OrganizationStatusType Status { get; set; }

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
