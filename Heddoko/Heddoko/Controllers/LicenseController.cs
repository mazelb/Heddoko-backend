﻿using DAL;
using Heddoko.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    [Auth(Roles = DAL.Constants.Roles.Admin)]
    public class LicenseController : BaseController
    {
        public LicenseController() : base() { }

        public LicenseController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

        public ActionResult Index()
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