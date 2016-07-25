using System.Web.Mvc;
using Heddoko.Models;

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