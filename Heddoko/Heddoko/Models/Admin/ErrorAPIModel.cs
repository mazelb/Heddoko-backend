using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ErrorAPIModel
    {
        public ErrorAPIType Type { get; set; }

        public string Key { get; set; }

        public string Message { get; set; }
    }
}