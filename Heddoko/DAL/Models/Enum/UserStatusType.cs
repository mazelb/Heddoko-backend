using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum UserStatusType
    {
        NotActive = 0,
        Active = 1,
        Banned = 2,
        Deleted = 3,
        Invited = 4
    }
}
