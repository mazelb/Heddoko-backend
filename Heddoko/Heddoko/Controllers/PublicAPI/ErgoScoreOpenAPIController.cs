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
            if (!id.HasValue)
            {
                id = CurrentUser.Id;
            }

            ErgoScore ergoScore = new ErgoScore();

            ergoScore.Score = UoW.AnalysisFrameRepository.GetUserScore(id.Value);
            ergoScore.Id = id.Value;

            return ergoScore;
        }

        [DomainRoute("org/{id:int}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            Organization org = UoW.OrganizationRepository.Get(orgId);
            IEnumerable<int> ids = org.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        [DomainRoute("team/{id:int?}", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            Team team = UoW.TeamRepository.Get(teamId);
            IEnumerable<int> ids = team.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        [DomainRoute("orgScore", ConfigKeyName.PublicApiSite)]
        [HttpGet]
        public ErgoScore GetCurrentOrgScore()
        {
            ErgoScore ergoScore = new ErgoScore();

            Organization org = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);
            if (org.Users != null)
            {
                IEnumerable<int> users = org.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.Id = org.Id;
            }

            return ergoScore;
        }

        [DomainRoute("teamScore", ConfigKeyName.DashboardSite)]
        [HttpGet]
        public ErgoScore GetCurrentTeamScore(int teamId)
        {
            ErgoScore ergoScore = new ErgoScore();

            Team team = UoW.TeamRepository.Get(teamId);
            if(team.Users != null)
            {
                IEnumerable<int> users = team.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.Id = teamId;
            }

            return ergoScore;
        }
    }
}