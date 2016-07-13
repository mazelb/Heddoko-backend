using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum IssueType
    {
        [StringValue("[FEATURES]")]
        NewFeature,
        [StringValue("[HARDWARE BUGS]")]
        Hardware,
        [StringValue("[SOFTWARE BUGS]")]
        Software
    }
}
