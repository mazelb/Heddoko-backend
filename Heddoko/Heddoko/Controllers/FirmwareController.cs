/**
 * @file FirmwareController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Web.Mvc;
using DAL;
using Heddoko.Models;
using Services;

namespace Heddoko.Controllers
{
    [Auth(Roles = Constants.Roles.Admin)]
    public class FirmwareController : BaseController
    {
        public FirmwareController() : base() { }

        public FirmwareController(ApplicationUserManager userManager, UnitOfWork uow)
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