using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DAL.Models;
using i18n;
using System.Web;

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

        public bool IsAdmin => IsAuth && CurrentUser.Role == UserRoleType.Admin;

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