using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using DAL.Models.MongoDocuments;
using Heddoko.Helpers.DomainRouting.Http;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/teamscore")]
    [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
    public class TeamScoreController : BaseAdminController<ErgoScore, ErgoScoreAPIModel>
    {
        public override KendoResponse<IEnumerable<ErgoScoreAPIModel>> Get([FromUri] KendoRequest request)
        {
            List<ErgoScoreAPIModel> scores = new List<ErgoScoreAPIModel>();

            if (CurrentUser.TeamID.HasValue)
            {
                List<int> users = UoW.UserRepository.GetIdsByTeam(CurrentUser.TeamID.Value).ToList();
                var results = UoW.ErgoScoreRecordRepository.GetMultipleUserScores(users.ToArray());

                scores.AddRange(results.ToList().Select(Convert));
            }

            int count = scores.Count();

            return new KendoResponse<IEnumerable<ErgoScoreAPIModel>>
            {
                Response = scores,
                Total = count
            };
        }

        protected override ErgoScoreAPIModel Convert(ErgoScore item)
        {
            if (item == null)
            {
                return null;
            }

            return new ErgoScoreAPIModel
            {
                ID = item.Id.Value,
                Score = item.Score,
                Name = UoW.UserRepository.Get(item.Id.Value).Name
            };
        }
    }
}