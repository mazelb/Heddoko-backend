using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Models;
using Services;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.All)]
    public class DefaultController : BaseController
    {
        public DefaultController() : base() { }

        public DefaultController(ApplicationUserManager userManager, UnitOfWork uow)
            : base(userManager, uow)
        {
        }

        public ActionResult Index()
        {
            DefaultIndexViewModel model = new DefaultIndexViewModel()
            {
                Software = UoW.FirmwareRepository.LastFirmwareByType(FirmwareType.Software),
                Guide = UoW.FirmwareRepository.LastFirmwareByType(FirmwareType.Guide),
                UserErgoScore = UoW.AnalysisFrameRepository.GetUserScore(CurrentUser.Id),
                EnableKendo = true
            };

            return View(model);
        }
    }
}