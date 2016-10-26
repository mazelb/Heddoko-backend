using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;
using Newtonsoft.Json;
using Services;

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
                Organization org = UoW.OrganizationRepository.GetIDCached(CurrentUser.OrganizationID.Value);
                int[] users = new int[org.Users.Count];
                for (int i = 0; i < users.Length; i++)
                {
                    users[i] = org.Users.ToArray()[i].Id;
                }
                scores.OrgScore = UoW.ProcessedFrameRepository.GetMultiUserScore(users);
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