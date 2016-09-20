using System;
using System.ComponentModel.DataAnnotations;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class TeamAPIModel : BaseAPIModel
    {
        public TeamAPIModel()
        {
            
        }
        public TeamAPIModel(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        private bool IsEmpty { get; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(255, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Address { get; set; }

        [StringLength(1024, ErrorMessageResourceName = "ValidateMaxLengthMessage", ErrorMessageResourceType = typeof(Resources))]
        public string Notes { get; set; }

        public TeamStatusType Status { get; set; }

        public string IDView { get; set; }

        public Organization Organization { get; set; }

        public string NameView => IsEmpty ? $"{Resources.No} {Resources.Team}" : $"{Name}";
    }
}