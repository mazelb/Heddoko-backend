using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class BaseViewModel
    {
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
    }
}