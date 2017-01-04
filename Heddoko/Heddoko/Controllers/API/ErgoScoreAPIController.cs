using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Helpers.DomainRouting.Http;
using Heddoko.Models;
using i18n;
using Services;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/ergoscore")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErgoScoreAPIController : BaseAPIController
    {
        private ErgoScoreService ergoScoreService;

        public ErgoScoreAPIController()
        {
            ergoScoreService = new ErgoScoreService(UoW, CurrentUser);
        }

        public ErgoScoreAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow)
        {
            ergoScoreService = new ErgoScoreService(uow, CurrentUser);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [DomainRoute("{id:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore Get(int? id = null)
        {
            return ergoScoreService.Get(id);
        }

        [DomainRoute("org/{orgId:int}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseUniversal)]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            return ergoScoreService.GetOrgScores(orgId);
        }

        [DomainRoute("team/{teamId:int}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            return ergoScoreService.GetTeamScores(teamId);
        }

        [DomainRoute("orgScore", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore GetCurrentOrgScore()
        {
            return ergoScoreService.GetCurrentOrgScore();
        }

        [DomainRoute("teamScore", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore GetCurrentTeamScore()
        {
            return ergoScoreService.GetCurrentTeamScore();
        }
    }
}