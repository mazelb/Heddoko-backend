/**
 * @file LicenseUniversalAPIController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Linq;
using System.Web.Http;
using DAL;
using DAL.Models;
using Heddoko.Models;
using Heddoko.Helpers.DomainRouting.Http;

namespace Heddoko.Controllers.API
{
    [AuthAPI(Roles = Constants.Roles.LicenseUniversal)]
    [RoutePrefix("api/v1/universal")]
    public class LicenseUniversalAPIController : BaseAPIController
    {
        [DomainRoute("organizations/{take:int}/{skip:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public ListAPIViewModel<Organization> Organizations(int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Organization>
            {
                Collection = UoW.OrganizationRepository.GetAllAPI(take, skip).ToList(),
                TotalCount = UoW.OrganizationRepository.GetAllAPICount()
            };
        }

        [DomainRoute("organizations/{organizationId:int}/teams/{take:int}/{skip:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public ListAPIViewModel<Team> Teams(int organizationId, int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Team>
            {
                Collection = UoW.TeamRepository.GetByOrganizationAPI(organizationId, take, skip).ToList(),
                TotalCount = UoW.TeamRepository.GetByOrganizationCount(organizationId)
            };
        }

        [DomainRoute("teams/{teamId:int}/records/{take:int}/{skip:int?}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public ListAPIViewModel<Record> Records(int teamId, int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Record>
            {
                Collection = UoW.RecordRepository.GetRecordsByTeam(teamId, take, skip).ToList(),
                TotalCount = UoW.RecordRepository.GetRecordsByTeamCount(teamId)
            };
        }
    }
}