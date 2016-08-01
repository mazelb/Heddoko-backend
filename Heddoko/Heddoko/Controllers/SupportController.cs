using DAL;
using DAL.Models;
using Heddoko.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
                if (model.Attachments?.Any() != null 
                 && model.Attachments.Sum(c => c?.ContentLength) > Constants.EmailLimit)
                {
                    ModelState.AddModelError(string.Empty, i18n.Resources.WrongAttachmentSize);
                }

                Mailer.SendSupportEmail(model);

                BaseViewModel modelStatus = new BaseViewModel();

                modelStatus.Flash.Add(new FlashMessage()
                {
                    Type = FlashMessageType.Success,
                    Message = i18n.Resources.SupportSent
                });

                return View("IndexStatus", modelStatus);
            }
            return View(model);
        }
    }
}