using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DAL;
using System.Web;

namespace Heddoko.Models
{
    public class SupportIndexViewModel : BaseViewModel
    {
        [Display(Name = "IssueType", ResourceType = typeof(i18n.Resources))]
        public IssueType Type { get; set; }

        [Display(Name = "Importance", ResourceType = typeof(i18n.Resources))]
        public IssueImportance Importance { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "ShortDescription", ResourceType = typeof(i18n.Resources))]
        [MaxLength(100)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "DetailedDescription", ResourceType = typeof(i18n.Resources))]
        [MaxLength(1000)]
        public string DetailedDescription { get; set; }

        [Display(Name = "Attachments", ResourceType = typeof(i18n.Resources))]
        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "ValidateEmailMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "Email", ResourceType = typeof(i18n.Resources))]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [Display(Name = "FullName", ResourceType = typeof(i18n.Resources))]
        [MaxLength(50)]
        public string FullName { get; set; }

        public DateTime Entered { get; set; }

        public IEnumerable<SelectListItem> ListIssueTypes
        {
            get
            {
                return new List<SelectListItem>()
                {
                        new SelectListItem { Text = DAL.Models.IssueType.NewFeature.GetDisplayName(), Value = ((int)DAL.Models.IssueType.NewFeature).ToString() },
                        new SelectListItem { Text = DAL.Models.IssueType.Hardware.GetDisplayName(), Value = ((int)DAL.Models.IssueType.Hardware).ToString(), Selected = true },
                        new SelectListItem { Text = DAL.Models.IssueType.Software.GetDisplayName(), Value = ((int)DAL.Models.IssueType.Software).ToString() }
                };
            }
        }

        public IEnumerable<SelectListItem> ListIssueImportances
        {
            get
            {
                return new List<SelectListItem>()
                {
                        new SelectListItem { Text = DAL.Models.IssueImportance.Low.GetDisplayName(), Value = ((int)DAL.Models.IssueImportance.Low).ToString(), Selected = true },
                        new SelectListItem { Text = DAL.Models.IssueImportance.Medium.GetDisplayName(), Value = ((int)DAL.Models.IssueImportance.Medium).ToString() },
                        new SelectListItem { Text = DAL.Models.IssueImportance.High.GetDisplayName(), Value = ((int)DAL.Models.IssueImportance.High).ToString() }
                };
            }
        }
    }
}
