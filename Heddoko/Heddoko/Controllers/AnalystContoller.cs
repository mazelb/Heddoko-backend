using System.Web.Mvc;
using DAL;
using Heddoko.Models;
using Services;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.AnalystAndAdmin)]
    public class AnalystController : BaseController
    {
        public AnalystController() : base() { }

        public AnalystController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            return RedirectToAction("Index", "Default");
        }
    }
}