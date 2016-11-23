using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DAL;
using DAL.Models;

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

        [Route("default")]
        [HttpGet]
        public Record DefaultRecord()
        {
            return UoW.RecordRepository.GetDefaultRecord();
        }
    }
}