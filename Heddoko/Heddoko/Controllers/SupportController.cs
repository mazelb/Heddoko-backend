using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;

namespace Heddoko.Controllers
{
    public class SupportController : BaseController
    {
        public ActionResult Index()
        {
            SupportIndexViewModel model = new SupportIndexViewModel();
            if (IsAuth)
            {
                model.Email = CurrentUser.Email;
                model.FullName = CurrentUser.Name;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SupportIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Attachments != null
                    &&
                    model.Attachments.Sum(c => c.ContentLength) > Constants.EmailLimit)
                {
                    ModelState.AddModelError(string.Empty, Resources.WrongAttachmentSize);
                }

                Mailer.SendSupportEmail(model);

                BaseViewModel modelStatus = new BaseViewModel();

                modelStatus.Flash.Add(new FlashMessage
                {
                    Type = FlashMessageType.Success,
                    Message = Resources.SupportSent
                });

                return View("IndexStatus", modelStatus);
            }
            return View(model);
        }
    }
}