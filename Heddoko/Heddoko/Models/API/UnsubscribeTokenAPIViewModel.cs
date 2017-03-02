/**
 * @file UnsubscribeTokenAPIViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using i18n;

namespace Heddoko.Models.API
{
    public class UnsubscribeTokenAPIViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(255)]
        public string Token { get; set; }
    }
}