/**
 * @file ErgoScoreController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Heddoko.Models;
using System.Web.Mvc;
using Heddoko.Controllers.API;
using DAL;
using DAL.Models;

namespace Heddoko.Controllers
{
    public class ErgoScoreController : BaseController
    {
        public ActionResult Index()
        {
            ErgoScoreViewModel model = new ErgoScoreViewModel()
            {
                UserOrganization = CurrentUser.Organization,
                UserTeam = CurrentUser.Team,
                EnableKendo = true
            };

            return View(model);
        }
    }
}