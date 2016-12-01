using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/ergoscore")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErgoScoreAPIController : BaseAPIController
    {
        public ErgoScoreAPIController() { }

        public ErgoScoreAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        [Route("{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScoreAPIModel Get(int? id = null)
        {
            ErgoScoreAPIModel ergoScore = new ErgoScoreAPIModel();

            ergoScore.Score = UoW.AnalysisFrameRepository.GetUserScore(id.Value);
            ergoScore.ID = id.Value;

            return ergoScore;
        }

        [Route("org/{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            Organization org = UoW.OrganizationRepository.Get(orgId);
            IEnumerable<int> ids = org.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        [Route("get")]
        [HttpPost]
        public ErgoScoreAPIModel GetErgoScoreModel()
        {
            ErgoScoreAPIModel score = new ErgoScoreAPIModel();

            score.Score = UoW.AnalysisFrameRepository.GetUserScore(CurrentUser.Id);
            score.ID = CurrentUser.Id;

            return score;
        }

        [Route("team/{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            Team team = UoW.TeamRepository.Get(teamId);
            IEnumerable<int> ids = team.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        public ErgoScore GetOrgScore(int orgId)
        {
            ErgoScore ergoScore = new ErgoScore();

            Organization org = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);
            if (org.Users != null)
            {
                IEnumerable<int> users = org.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.ID = org.Id;
            }

            return ergoScore;
        }

        public ErgoScore GetTeamScore(int teamId)
        {
            ErgoScore ergoScore = new ErgoScore();

            Team team = UoW.TeamRepository.Get(teamId);
            if(team.Users != null)
            {
                IEnumerable<int> users = team.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.ID = teamId;
            }

            return ergoScore;
        }
    }
}