using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class OrganizationAPIModel : BaseAPIModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Address { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public string Notes { get; set; }

        public int? UserID { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        public UserAPIModel User { get; set; }

        public int? DataCollectorAmount { get; set; }

        public int? DataAnalysisAmount { get; set; }

        public OrganizationStatusType Status { get; set; }
    }
}