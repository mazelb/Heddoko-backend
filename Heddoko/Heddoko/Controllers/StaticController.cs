using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    public class StaticController : Controller
    {
        public ActionResult Terms()
        {
            return View(new BaseViewModel());
        }

        public ActionResult Privacy()
        {
            return View(new BaseViewModel());
        }
    }
}