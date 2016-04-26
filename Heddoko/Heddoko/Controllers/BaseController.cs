using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    public class BaseController : Controller
    {
        protected HDContext db { get; set; }

        public BaseController()
        {
            db = new HDContext();
            CurrentUser = Forms.ValidateSession(db);
            ContextTempData = new ContextTempData(TempData);
        }

        protected ContextTempData ContextTempData { get; set; }

        protected User CurrentUser { get; set; }

        protected bool IsAuth
        {
            get
            {
                return CurrentUser != null;
            }
        }
    }
}