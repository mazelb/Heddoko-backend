using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models.MongoDocuments;
using Heddoko.Models;
using Services;

namespace Heddoko.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("admin/api/ergoscorerecord")]
    [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
    public class ErgoScoreRecordController : BaseAdminController<ErgoScoreRecord, ErgoScoreRecordAPIModel>
    {
        private const string Search = "Search";

        public override KendoResponse<IEnumerable<ErgoScoreRecordAPIModel>> Get([FromUri] KendoRequest request)
        {
            List<ErgoScoreRecordAPIModel> records = new List<ErgoScoreRecordAPIModel>();

            if (CurrentUser.TeamID.HasValue)
            {
                int[] users = new int[0];

                if (request?.Filter != null)
                {
                    KendoFilterItem searchFilter = request.Filter.Get(Search);
                    if (!string.IsNullOrEmpty(searchFilter?.Value))
                    {
                        string[] tempIDs = searchFilter.Value.Split(',');
                        users = tempIDs.Select(userID => Int32.Parse(userID)).ToArray();
                    }                  
                }
                else
                {
                    users = UoW.UserRepository.GetIdsByTeam(CurrentUser.TeamID.Value).ToArray();                
                }

                var results = UoW.ErgoScoreRecordRepository.GetTeamErgoScoreRecords(users);

                records.AddRange(results.ToList().Select(Convert));
            }
            int count = records.Count();

            return new KendoResponse<IEnumerable<ErgoScoreRecordAPIModel>>
            {
                Response = records,
                Total = count
            };
        }

        protected override ErgoScoreRecordAPIModel Convert(ErgoScoreRecord item)
        {
            if (item == null)
            {
                return null;
            }
            return new ErgoScoreRecordAPIModel
            {
                UserId = item.UserId,
                RecordScore = item.RecordScore,
                HourlyScores = item.HourlyScores,
                Date = item.StartTime.ConvertFromUnixTimestamp()
            };
        }
    }
}