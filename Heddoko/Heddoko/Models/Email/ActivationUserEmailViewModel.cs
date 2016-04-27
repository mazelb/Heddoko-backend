using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ActivationUserEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string Token { get; set; }
    }
}