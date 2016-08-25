using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class SupportIndexViewModel : BaseViewModel
    {
        [Display(Name = "IssueType", ResourceType = typeof(Resources))]
        public IssueType Type { get; set; }

        [Display(Name = "Importance", ResourceType = typeof(Resources))]
        public IssueImportance Importance { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "ShortDescription", ResourceType = typeof(Resources))]
        [MaxLength(100)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "DetailedDescription", ResourceType = typeof(Resources))]
        [MaxLength(1000)]
        public string DetailedDescription { get; set; }

        [Display(Name = "Attachments", ResourceType = typeof(Resources))]
        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = null, ErrorMessageResourceName = "ValidateEmailMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [Display(Name = "FullName", ResourceType = typeof(Resources))]
        [MaxLength(50)]
        public string FullName { get; set; }

        public DateTime Entered { get; set; }

        public IEnumerable<SelectListItem> ListIssueTypes
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = IssueType.NewFeature.GetDisplayName(),
                        Value = ((int) IssueType.NewFeature).ToString()
                    },
                    new SelectListItem
                    {
                        Text = IssueType.Hardware.GetDisplayName(),
                        Value = ((int) IssueType.Hardware).ToString(),
                        Selected = true
                    },
                    new SelectListItem
                    {
                        Text = IssueType.Software.GetDisplayName(),
                        Value = ((int) IssueType.Software).ToString()
                    }
                };
            }
        }

        public IEnumerable<SelectListItem> ListIssueImportances
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = IssueImportance.Low.GetDisplayName(),
                        Value = ((int) IssueImportance.Low).ToString(),
                        Selected = true
                    },
                    new SelectListItem
                    {
                        Text = IssueImportance.Medium.GetDisplayName(),
                        Value = ((int) IssueImportance.Medium).ToString()
                    },
                    new SelectListItem
                    {
                        Text = IssueImportance.High.GetDisplayName(),
                        Value = ((int) IssueImportance.High).ToString()
                    }
                };
            }
        }
    }
}