/**
 * @file SubscribeTokenAPIViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using DAL.Models.Enum;

namespace Heddoko.Models.API
{
    public class SubscribeTokenAPIViewModel
    {
        [Required(ErrorMessageResourceName = "ValidateRequiredMessage", ErrorMessageResourceType = typeof(i18n.Resources))]
        [MaxLength(255)]
        public string Token { get; set; }

        public DeviceType Type { get; set; }
    }
}