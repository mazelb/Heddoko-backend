using System.Linq;
using System.Web.Http;
using DAL;
using DAL.Models;
using Heddoko.Models;

namespace Heddoko.Controllers.API
{
    [AuthAPI(Roles = Constants.Roles.All)]
    [RoutePrefix("api/v1/records")]
    public class RecordsAPIController : BaseAPIController
    {
        public RecordsAPIController()
        {
        }

        public RecordsAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow)
        {
        }

        [Route("default/{take:int}/{skip:int?}")]
        [HttpGet]
        public ListAPIViewModel<Record> DefaultRecords(int take = 100, int? skip = 0)
        {
            return new ListAPIViewModel<Record>
            {
                Collection = UoW.RecordRepository.GetDefaultRecords(take, skip).ToList(),
                TotalCount = UoW.RecordRepository.GetDefaultRecordsCount()
            };
        }
    }
}