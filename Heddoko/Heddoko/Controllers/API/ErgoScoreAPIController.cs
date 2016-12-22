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

namespace Heddoko.Controllers.API
{
    [RoutePrefix("admin/api/ergoscore")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErgoScoreAPIController : BaseAPIController
    {
        public ErgoScoreAPIController() { }

        public ErgoScoreAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }


        [ApiExplorerSettings(IgnoreApi = true)]
        [DomainRoute("{id:int}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore Get(int id)
        {
            ErgoScore ergoScore = new ErgoScore();

            ergoScore.Score = UoW.AnalysisFrameRepository.GetUserScore(id);
            ergoScore.Id = id;

            return ergoScore;
        }

        [DomainRoute("org/{id:int}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseUniversal)]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            Organization org = UoW.OrganizationRepository.Get(orgId);
            IEnumerable<int> ids = org.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        [DomainRoute("team/{id:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            Team team = UoW.TeamRepository.Get(teamId);
            IEnumerable<int> ids = team.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        [DomainRoute("orgScore", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
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

        [DomainRoute("teamScore", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
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