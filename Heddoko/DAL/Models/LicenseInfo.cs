using System;

namespace DAL.Models
{
    public class LicenseInfo
    {
        public LicenseStatusType Status { get; set; }

        public LicenseType Type { get; set; }

        public DateTime ExpiredAt { get; set; }

        public string Name { get; set; }

        public string ViewID { get; set; }

        public int ID { get; set; }
    }
}