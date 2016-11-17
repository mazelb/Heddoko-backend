using System.Linq;
using System.Web.Http;
using DAL;
using DAL.Models;
using Heddoko.Models;

namespace Heddoko.Controllers.API
{
    [AuthAPI(Roles = Constants.Roles.LicenseUniversal)]
    [RoutePrefix("api/v1")]
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

        [Route("teams/{take:int}/{skip:int?}")]
        [HttpGet]
        public ListAPIViewModel<Team> Teams(int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Team>
            {
                Collection = UoW.TeamRepository.GetAllAPI(take, skip).ToList(),
                TotalCount = UoW.TeamRepository.GetAllAPICount()
            };
        }

        [Route("records/{take:int}/{skip:int?}")]
        [HttpGet]
        public ListAPIViewModel<Asset> Records(int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Asset>
            {
                Collection = UoW.AssetRepository.GetAllRecords(take, skip).ToList(),
                TotalCount = UoW.AssetRepository.GetAllRecordsCount()
            };
        }
    }
}