using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Heddoko.Models;

namespace Heddoko.Controllers
{
    public class ErgoScoreController : BaseController
    {

        public async Task<KendoResponse<ErgoScoreAPIModel>> Get([FromUri] KendoRequest request)
        {
            ErgoScoreAPIModel scores = new ErgoScoreAPIModel();

            scores.UserScore = await UoW.ProcessedFrameRepository.GetUserScoreAsync(CurrentUser.Id);

            if (CurrentUser.OrganizationID.HasValue)
            {
                IEnumerable<int> users = UoW.UserRepository.GetIdsByOrganization(CurrentUser.OrganizationID.Value);
                scores.OrgScore = await UoW.ProcessedFrameRepository.GetMultiUserScoreAsync(users.ToArray());
            }
            else
            {
                scores.OrgScore = 0;
            }

            return new KendoResponse<ErgoScoreAPIModel>
            {
                Response = scores
            };
        }

        public async Task<KendoResponse<double>> Get(int id)
        {
            double score = await UoW.ProcessedFrameRepository.GetUserScoreAsync(id);

            return new KendoResponse<double>
            {
                Response = score
            };
        }

        public async Task<KendoResponse<double>> Get(int[] ids)
        {
            double score = await UoW.ProcessedFrameRepository.GetMultiUserScoreAsync(ids);

            return new KendoResponse<double>
            {
                Response = score
            };
        }
    }
}