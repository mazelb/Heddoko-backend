using System.Web.Mvc;
using DAL;
using Heddoko.Models;
using Services;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.Admin)]
    public class AdminController : BaseController
    {
        public AdminController() : base() { }

        public AdminController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

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