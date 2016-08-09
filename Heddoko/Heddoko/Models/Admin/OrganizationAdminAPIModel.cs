using System.ComponentModel.DataAnnotations;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class OrganizationAdminAPIModel
    {
        public int UserID { get; set; }

        public int OrganizationID { get; set; }
    }
}