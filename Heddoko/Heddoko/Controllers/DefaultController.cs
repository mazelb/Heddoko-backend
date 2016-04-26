using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Heddoko.Controllers
{
    public class DefaultController : BaseController
    {
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }
    }
}