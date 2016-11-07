using DAL;
using Heddoko.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    [Auth(Roles = DAL.Constants.Roles.All)]
    public class DevelopmentController : BaseController
    {
        public DevelopmentController() : base() { }

        public DevelopmentController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel
            {
                Title = i18n.Resources.Development,
                EnableKendo = true
            };
            return View(model);
        }

        [Auth(Roles = DAL.Constants.Roles.Admin)]
        public ActionResult Approve()
        {
            BaseViewModel model = new BaseViewModel
            {
                Title = i18n.Resources.AdminTitle,
                EnableKendo = true
            };
            return View(model);
        }

    }
}