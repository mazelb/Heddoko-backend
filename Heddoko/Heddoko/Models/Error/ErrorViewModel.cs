using System;

namespace Heddoko.Models
{
    public class ErrorViewModel : BaseViewModel
    {
        public Exception Ex { get; set; }

        public string ExMessage => Ex?.Message ?? Message;

        public string Url { get; set; }
        public string Message { get; set; }
    }
}