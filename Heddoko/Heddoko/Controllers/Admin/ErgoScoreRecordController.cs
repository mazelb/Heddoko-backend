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
        private const string Users = "Users";
        private const string Dates = "Dates";

        public override KendoResponse<IEnumerable<ErgoScoreRecordAPIModel>> Get([FromUri] KendoRequest request)
        {
            List<ErgoScoreRecordAPIModel> records = new List<ErgoScoreRecordAPIModel>();

            if (CurrentUser.TeamID.HasValue)
            {
                List<int> users = null;
                List<int?> dates = new List<int?> { null, null };

                if (request?.Filter != null)
                {
                    KendoFilterItem usersFilter = request.Filter.Get(Users);
                    if (!string.IsNullOrEmpty(usersFilter?.Value))
                    {
                        string[] tempIDs = usersFilter.Value.Split(',');
                        users = tempIDs.Select(userID => Int32.Parse(userID)).ToList();
                    }

                    KendoFilterItem datesFilter = request.Filter.Get(Dates);
                    if (!string.IsNullOrEmpty(datesFilter?.Value))
                    {
                        //datesFilter.Value is an array [start, end] either can be null
                        string[] tempDates = datesFilter.Value.Split(',');
                        for (int i = 0; i < 2; i++)
                        {
                            if(!(tempDates[i].IsNullOrEmpty() || tempDates[i] == "null"))
                            {
                                dates[i] = Int32.Parse(tempDates[i]);
                            }
                        }
                    }        
                }
                if (users == null)
                {
                    users = UoW.UserRepository.GetIdsByTeam(CurrentUser.TeamID.Value).ToList();
                }

                var results = UoW.ErgoScoreRecordRepository.GetFilteredErgoScoreRecords(users.ToArray(), dates.ToArray());

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