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
    [System.Web.Http.RoutePrefix("analyst/api/teamscore")]
    [AuthAPI(Roles = Constants.Roles.All)]
    public class TeamScoreController : BaseAdminController<ErgoScore, ErgoScoreAPIModel>
    {
        public override KendoResponse<IEnumerable<ErgoScoreAPIModel>> Get([FromUri] KendoRequest request)
        {
            List<ErgoScoreAPIModel> scores = new List<ErgoScoreAPIModel>();

            if (CurrentUser.OrganizationID.HasValue)
            {
                List<User> Users = UoW.UserRepository.GetByOrganization(CurrentUser.OrganizationID.Value).ToList();
                ErgoScoreAPIModel temp;
                foreach (User user in Users)
                {
                    if (user.RoleType == UserRoleType.Worker)
                    {
                        temp = new ErgoScoreAPIModel();
                        temp.ErgoScore = UoW.ProcessedFrameRepository.GetUserScore(user.Id);
                        temp.ID = user.Id;
                        scores.Add(temp);
                    }
                }
            }

            return new KendoResponse<IEnumerable<ErgoScoreAPIModel>>
            {
                Response = scores
            };
        }
    }
}