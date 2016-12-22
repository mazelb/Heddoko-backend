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
    [RoutePrefix("admin/api/ergoscore")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErgoScoreAPIController : BaseAPIController
    {
        public ErgoScoreAPIController() { }

        public ErgoScoreAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }


        [ApiExplorerSettings(IgnoreApi = true)]
        [DomainRoute("{id:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore Get(int? id = null)
        {
            return ErgoScoreService.Get(id);
        }

        [DomainRoute("org/{id:int}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseUniversal)]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            return ErgoScoreService.GetOrgScores(orgId);
        }

        [DomainRoute("team/{id:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            return ErgoScoreService.GetTeamScores(teamId);
        }

        [DomainRoute("orgScore", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore GetCurrentOrgScore()
        {
            return ErgoScoreService.GetCurrentOrgScore();
        }

        [DomainRoute("teamScore", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore GetCurrentTeamScore(int teamId)
        {
            return ErgoScoreService.GetCurrentTeamScore(teamId);
        }
    }
}