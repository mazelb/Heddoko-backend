/**
 * @file TeamScoreController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 1 2017
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
    [RoutePrefix("admin/api/ergoscoreLeaderboard")]
    [AuthAPI(Roles = Constants.Roles.LicenseAdminAndAnalystAndAdmin)]
    public class ErgoScoreLeaderboardController : BaseAdminController<ErgoScore, ErgoScoreAPIModel>
    {
        private const string Users = "Users";
        private const string Teams = "Teams";

        public override KendoResponse<IEnumerable<ErgoScoreAPIModel>> Get([FromUri] KendoRequest request)
        {
            List<ErgoScoreAPIModel> scores = new List<ErgoScoreAPIModel>();
            List<int> users = null;

            if (request?.Filter != null)
            {
                KendoFilterItem usersFilter = request.Filter.Get(Users);
                KendoFilterItem teamsFilter = request.Filter.Get(Teams);

                if (!string.IsNullOrEmpty(usersFilter?.Value))
                {
                    string[] tempIDs = usersFilter.Value.Split(',');
                    users = tempIDs.Select(userID => Int32.Parse(userID)).ToList();
                }  
                // Don't want to filter by team if we already filter by user            
                else if (!string.IsNullOrEmpty(teamsFilter?.Value))
                {
                    users = new List<int>();
                    string[] tempIDs = teamsFilter.Value.Split(',');
                    List<int> teams = tempIDs.Select(teamID => Int32.Parse(teamID)).ToList();
                    foreach (int team in teams)
                    {
                        users.AddRange(UoW.UserRepository.GetIdsByTeam(team));
                    }
                }
            }

            else if (IsLicenseAdmin && CurrentUser.OrganizationID.HasValue)
            {
                users = UoW.UserRepository.GetIdsByOrganization(CurrentUser.OrganizationID.Value).ToList();
            }

            else if (IsAnalyst && CurrentUser.TeamID.HasValue)
            {
                users = UoW.UserRepository.GetIdsByTeam(CurrentUser.TeamID.Value).ToList();
            }

            if (users != null)
            {
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