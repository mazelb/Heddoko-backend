using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using Jil;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        [ForeignKey("RoleId")]
        [JilDirective(Ignore = true)]
        [JsonIgnore]
        public virtual IdentityRole Role { get; set; }
    }
}
