using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    [Auth(Roles = DAL.Constants.Roles.Admin)]
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            model.Title = i18n.Resources.AdminTitle;
            model.EnableKendo = true;
            return View(model);
        }
    }
}