/**
 * @file AuthorizeViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using i18n;

namespace Heddoko.Models
{
    public class AuthorizeViewModel : BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Client { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ClientSecret { get; set; }
    }
}