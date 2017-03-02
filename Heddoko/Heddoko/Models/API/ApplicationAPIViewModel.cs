/**
 * @file ApplicationAPIViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class ApplicationAPIViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Client { get; set; }

        public string Secret { get; set; }

        public string RedirectUrl { get; set; }

        public bool Enabled { get; set; }
    }
}