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

        [Route("{id:int}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore Get(int id)
        {
            ErgoScore ergoScore = new ErgoScore();

            ergoScore.Score = UoW.ErgoScoreRecordRepository.GetUserScore(id);
            ergoScore.Id = id;

            return ergoScore;
        }

        [Route("team/{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            IEnumerable<int> ids = UoW.UserRepository.GetIdsByTeam(teamId);

            return UoW.ErgoScoreRecordRepository.GetMultipleUserScores(ids.ToArray());
        }

        [Route("teamScore")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore GetTeamErgoScore(int teamId)
        {
            ErgoScore ergoScore = new ErgoScore();

            IEnumerable<int> users = UoW.UserRepository.GetIdsByTeam(teamId);
            ergoScore.Score = UoW.ErgoScoreRecordRepository.GetTeamScore(users.ToArray());
            ergoScore.Id = teamId;

            return ergoScore;
        }
    }
}