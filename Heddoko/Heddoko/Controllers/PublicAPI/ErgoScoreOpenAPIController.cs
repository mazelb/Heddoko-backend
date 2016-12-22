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
using Heddoko.Helpers.Auth;
using static DAL.Constants;
using Services;

namespace Heddoko.Controllers.API
{
    [ClaimsAuthorization(ClaimType = OpenAPIClaims.ClaimType, ClaimValue = OpenAPIClaims.ClaimValue)]
    [RoutePrefix("api/v1/ergoscore")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErgoScoreOpenAPIController : BaseAPIController
    {
        public ErgoScoreOpenAPIController() { }

        public ErgoScoreOpenAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }


        [ApiExplorerSettings(IgnoreApi = true)]
        [DomainRoute("{id:int?}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public ErgoScore Get(int? id = null)
        {
            return ErgoScoreService.Get(id);
        }

        [DomainRoute("org/{id:int}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            return ErgoScoreService.GetOrgScores(orgId);
        }

        [DomainRoute("team/{id:int?}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            return ErgoScoreService.GetTeamScores(teamId);
        }

        [DomainRoute("orgScore", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public ErgoScore GetCurrentOrgScore()
        {
            return ErgoScoreService.GetCurrentOrgScore();
        }

        [DomainRoute("teamScore", ConfigKeyName.DashboardSite)]
        [HttpGet]
        public ErgoScore GetCurrentTeamScore(int teamId)
        {
            return ErgoScoreService.GetCurrentTeamScore(teamId);
        }
    }
}