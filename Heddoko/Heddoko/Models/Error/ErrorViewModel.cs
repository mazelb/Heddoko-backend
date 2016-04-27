using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ErrorViewModel : BaseViewModel
    {
        public Exception Ex { get; set; }
        public string ExMessage
        {
            get
            {
                return Ex != null ? Ex.Message : Message;
            }
        }
        public string Url { get; set; }
        public string Message { get; set; }
    }
}