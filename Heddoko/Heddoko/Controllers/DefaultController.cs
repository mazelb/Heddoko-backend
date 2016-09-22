using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.All)]
    public class DefaultController : BaseController
    {
        public ActionResult Index()
        {
            DefaultIndexViewModel model = new DefaultIndexViewModel()
            {
                Software = UoW.FirmwareRepository.LastFirmwareByType(FirmwareType.Software),
                Guide = UoW.FirmwareRepository.LastFirmwareByType(FirmwareType.Guide)
            };

            return View(model);
        }
    }
}