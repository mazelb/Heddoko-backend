using System.Linq;
using System.Web.Http;
using DAL;
using DAL.Models;
using Heddoko.Models;

namespace Heddoko.Controllers.API
{
    [AuthAPI(Roles = Constants.Roles.LicenseUniversal)]
    [RoutePrefix("api/v1/universal")]
    public class LicenseUniversalAPIController : BaseAPIController
    {
        [Route("organizations/{take:int}/{skip:int?}")]
        [HttpGet]
        public ListAPIViewModel<Organization> Organizations(int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Organization>
            {
                Collection = UoW.OrganizationRepository.GetAllAPI(take, skip).ToList(),
                TotalCount = UoW.OrganizationRepository.GetAllAPICount()
            };
        }

        [Route("organizations/{organizationId:int}/teams/{take:int}/{skip:int?}")]
        [HttpGet]
        public ListAPIViewModel<Team> Teams(int organizationId, int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Team>
            {
                Collection = UoW.TeamRepository.GetByOrganizationAPI(organizationId, take, skip).ToList(),
                TotalCount = UoW.TeamRepository.GetByOrganizationCount(organizationId)
            };
        }

        [Route("teams/{teamId:int}/records/{take:int}/{skip:int?}")]
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