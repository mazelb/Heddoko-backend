using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Models
{
    public class BaseViewModel
    {
        private static IEnumerable<SelectListItem> _countries;

        public BaseViewModel()
        {
            Flash = new FlashMessagesViewModel();
            CurrentUser = ContextSession.User;
            Title = i18n.Resources.Title;
        }

        public string Title { get; set; }

        public bool EnableKendo { get; set; }

        public FlashMessagesViewModel Flash { get; set; }

        public User CurrentUser { get; set; }

        public bool IsAuth
        {
            get
            {
                return CurrentUser != null;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return IsAuth && CurrentUser.Role == UserRoleType.Admin;
            }
        }

        public IEnumerable<SelectListItem> ListCountries
        {
            get
            {
                if (_countries == null)
                {
                    _countries = new List<SelectListItem>()
                    {
                         new SelectListItem { Text = DAL.Models.CountryType.Canada.ToString(), Value = DAL.Models.CountryType.Canada.ToString() },
                         new SelectListItem { Text = DAL.Models.CountryType.USA.ToString(), Value = DAL.Models.CountryType.USA.ToString() }
                    };
                }
                return _countries;
            }
        }

        public string Greeting
        {
            get
            {
                int hour = DateTime.Now.Hour;
                string result = i18n.Resources.GoodMorning;
                if (hour > 11 && hour < 17)
                {
                    result = i18n.Resources.GoodAfternoon;
                }
                else if (hour >= 17)
                {
                    result = i18n.Resources.GoodEvening;
                }

                return result;
            }
        }
    }
}