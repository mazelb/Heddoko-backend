using System.Web.Mvc;
using DAL;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.AnalystAndAdmin)]
    public class AnalystController : BaseController
    {
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            return RedirectToAction("Index", "Default");
        }
    }
}