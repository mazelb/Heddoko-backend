/**
 * @file BaseViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Models;
using i18n;
using System.Web;
using DAL;

namespace Heddoko.Models
{
    public class BaseViewModel
    {
        private static IEnumerable<SelectListItem> _countries;

        public BaseViewModel()
        {
            Flash = new FlashMessagesViewModel();
            CurrentUser = Auth.CurrentUser;
            Title = Resources.Title;
        }

        public string Title { get; set; }

        public bool EnableKendo { get; set; }

        public FlashMessagesViewModel Flash { get; set; }

        public User CurrentUser { get; set; }

        public bool IsAuth => CurrentUser != null;

        public bool IsAdmin => IsAuth && (CurrentUser.Role == UserRoleType.Admin || CurrentUser.Role == UserRoleType.ServiceAdmin);

        public bool IsLicenseAdmin => IsAuth && CurrentUser.Role == UserRoleType.LicenseAdmin;

        public bool IsAnalyst => IsAuth && CurrentUser.Role == UserRoleType.Analyst;

        public bool IsWorker => IsAuth && CurrentUser.Role == UserRoleType.Worker;

        public IEnumerable<SelectListItem> ListCountries => _countries ?? (_countries = new List<SelectListItem>
        {
            new SelectListItem
            {
                Text = CountryType.Canada.ToString(),
                Value = CountryType.Canada.ToString()
            },
            new SelectListItem
            {
                Text = CountryType.USA.ToString(),
                Value = CountryType.USA.ToString()
            }
        });

        public static string Greeting
        {
            get
            {
                int hour = DateTime.Now.Hour;
                string result = Resources.GoodMorning;
                if (hour > 11 &&
                    hour < 17)
                {
                    result = Resources.GoodAfternoon;
                }
                else if (hour >= 17)
                {
                    result = Resources.GoodEvening;
                }

                return result;
            }
        }
    }
}