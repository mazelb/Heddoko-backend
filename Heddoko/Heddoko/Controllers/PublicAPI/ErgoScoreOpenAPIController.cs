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
using Heddoko.Controllers.API;

namespace Heddoko.Controllers.PublicAPI
{
    [ClaimsAuthorization(ClaimType = OpenAPIClaims.ClaimType, ClaimValue = OpenAPIClaims.ClaimValue)]
    [RoutePrefix("v1/ergoscore")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErgoScoreOpenAPIController : BaseAPIController
    {
        private ErgoScoreService ergoScoreService;

        public ErgoScoreOpenAPIController()
        {
            ergoScoreService = new ErgoScoreService(UoW, CurrentUser);
        }

        public ErgoScoreOpenAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow)
        {
            ergoScoreService = new ErgoScoreService(uow, CurrentUser);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [DomainRoute("{id:int?}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public ErgoScore Get(int? id = null)
        {
            return ergoScoreService.Get(id);
        }

        [DomainRoute("org/{id:int}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            return ergoScoreService.GetOrgScores(orgId);
        }

        [DomainRoute("team/{id:int?}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            return ergoScoreService.GetTeamScores(teamId);
        }

        [DomainRoute("orgScore", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public ErgoScore GetCurrentOrgScore()
        {
            return ergoScoreService.GetCurrentOrgScore();
        }

        [DomainRoute("teamScore", ConfigKeyName.DashboardSite)]
        [HttpGet]
        public ErgoScore GetCurrentTeamScore(int teamId)
        {
            return ergoScoreService.GetCurrentTeamScore(teamId);
        }
    }
}