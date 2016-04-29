using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    [Auth(Roles = DAL.Constants.Roles.AnalystAndAdmin)]
    public class AnalystController : BaseController
    {
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            return View(model);
        }
    }
}