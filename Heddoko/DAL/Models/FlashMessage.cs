using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FlashMessage
    {
        public FlashMessageType Type { get; set; }

        public string Message { get; set; }
    }
}
