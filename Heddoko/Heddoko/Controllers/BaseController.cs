using DAL;
using DAL.Models;
using Heddoko.Helpers.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : Controller
    {
        protected UnitOfWork UoW { get; set; }

        public BaseController()
        {
            UoW = new UnitOfWork();
            CurrentUser = Forms.ValidateSession(UoW);
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