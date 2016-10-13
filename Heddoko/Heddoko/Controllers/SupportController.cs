﻿using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using Hangfire;
using Heddoko.Models;
using i18n;
using Services;

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Attachments != null
                &&
                model.Attachments.Sum(c => c?.ContentLength) > Constants.EmailLimit)
            {
                ModelState.AddModelError(string.Empty, Resources.WrongAttachmentSize);
            }

            //Mailer.SendSupportEmail(model);

            var mailModel = new Services.MailSending.Models.SupportEmailViewModel
            {
                Type = model.Type,
                Importance = model.Importance,
                DetailedDescription = model.DetailedDescription,
                Email = model.Email,
                FullName = model.FullName,
                ShortDescription = model.ShortDescription,
                Attachments = model.Attachments
            };

            BackgroundJob.Enqueue(() => EmailManager.SendSupportEmail(mailModel));

            BaseViewModel modelStatus = new BaseViewModel();

            modelStatus.Flash.Add(new FlashMessage
            {
                Type = FlashMessageType.Success,
                Message = Resources.SupportSent
            });

            return View("IndexStatus", modelStatus);
        }
    }
}