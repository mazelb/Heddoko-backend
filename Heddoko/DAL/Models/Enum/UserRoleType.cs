using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum UserRoleType
    {
        [StringValue(Constants.Roles.Admin)]
        Admin = 0,
        [StringValue(Constants.Roles.Analyst)]
        Analyst = 1,
        [StringValue(Constants.Roles.User)]
        User = 2
    }
}
