using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.Models;
using Heddoko.Models;
using i18n;

namespace Heddoko.Controllers.API
{
    [RoutePrefix("api/v1/ergoscore")]
    public class ErgoScoreAPIController : BaseAPIController
    {
        public ErgoScoreAPIController() { }

        public ErgoScoreAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScoreAPIModel Get(int? id = null)
        {
            ErgoScoreAPIModel ergoScore = new ErgoScoreAPIModel();
            User user;

            if (!id.HasValue)
            {
                id = CurrentUser.Id;
                user = CurrentUser;
            }
            else
            {
                user = UoW.UserRepository.GetIDCached(id.Value);
            }
            ergoScore.UserScore = UoW.ProcessedFrameRepository.GetUserScore(id.Value);
            
            if (user.OrganizationID.HasValue)
            {
                IEnumerable<int> users = UoW.UserRepository.GetIdsByOrganization(user.OrganizationID.Value);
                ergoScore.OrgScore = UoW.ProcessedFrameRepository.GetMultiUserScore(users.ToArray());
            }
            else
            {
                ergoScore.OrgScore = 0;
            }


            return ergoScore;
        }

        [Route("get")]
        [HttpPost]
        public ErgoScoreAPIModel GetErgoScoreModel()
        {
            ErgoScoreAPIModel scores = new ErgoScoreAPIModel();

            scores.UserScore = UoW.ProcessedFrameRepository.GetUserScore(CurrentUser.Id);
            scores.OrgScore = 0;

            if (CurrentUser.OrganizationID.HasValue)
            {
                IEnumerable<int> users = UoW.UserRepository.GetIdsByOrganization(CurrentUser.OrganizationID.Value);
                scores.OrgScore = UoW.ProcessedFrameRepository.GetMultiUserScore(users.ToArray());
            }
            return scores;
        }
    }
}