using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum FlashMessageType
    {
        [StringValue("alert-success")]
        Success = 0,
        [StringValue("alert-info")]
        Info = 1,
        [StringValue("alert-warning")]
        Warning = 2,
        [StringValue("alert-danger")]
        Error = 3
    }
}
