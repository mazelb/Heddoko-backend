using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [System.Web.Http.RoutePrefix("api/teamscore")]
    [AuthAPI(Roles = Constants.Roles.All)]
    public class TeamScoreController : BaseAdminController<ErgoScore, ErgoScoreAPIModel>
    {
        public override KendoResponse<IEnumerable<ErgoScoreAPIModel>> Get([FromUri] KendoRequest request)
        {
            List<ErgoScoreAPIModel> scores = new List<ErgoScoreAPIModel>();

            if (CurrentUser.OrganizationID.HasValue)
            {
                List<User> users = UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value).ToList();
                IEnumerable<int> ids = users.Select(x => x.Id).Distinct();

                var results = UoW.ProcessedFrameRepository.GetMultipleUserScores(ids.ToArray());

                foreach (ErgoScore result in results)
                {
                    scores.Add(Convert(result));
                }
            }

            return new KendoResponse<IEnumerable<ErgoScoreAPIModel>>
            {
                Response = scores
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
                ID = item.ID,
                Score = item.Score
            };
        }
    }
}