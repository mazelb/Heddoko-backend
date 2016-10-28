using System;

namespace Services.MailSending.Models
{
    public class LicenseExpiringEmailViewModel : EmailViewModel
    {
        public int Days { get; set; }

        public DateTime ExpirationAt { get; set; }

        public string LicenseName { get; set; }

        public string OrganizationName { get; set; }

        public string Name { get; set; }
    }
}
