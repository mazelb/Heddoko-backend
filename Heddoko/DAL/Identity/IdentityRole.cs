using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class IdentityRole : IdentityRole<int, UserRole>
    {
        public IdentityRole() { }
        public IdentityRole(string name) { Name = name; }
    }
}
