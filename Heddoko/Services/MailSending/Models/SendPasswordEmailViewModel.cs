using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MailSending.Models
{
    public class SendPasswordEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string OrganizationName { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }
    }
}
