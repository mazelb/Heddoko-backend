using System.Web.Mvc;
using DAL;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.Admin)]
    public class FirmwareController : BaseController
    {
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel
            {
                EnableKendo = true
            };
            return View(model);
        }
    }
}