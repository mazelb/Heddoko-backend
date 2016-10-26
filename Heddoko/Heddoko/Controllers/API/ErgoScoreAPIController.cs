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

        public ErgoScoreAPIController(ApplicationUserManager userManager, UnitOfWork UoW) : base(userManager, UoW) { }

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
                Organization org = UoW.OrganizationRepository.GetIDCached(user.OrganizationID.Value);
                int[] users = new int[org.Users.Count];
                for (int i = 0; i < users.Length; i++)
                {
                    users[i] = org.Users.ToArray()[i].Id;
                }
                ergoScore.OrgScore = UoW.ProcessedFrameRepository.GetMultiUserScore(users);
            }
            else
            {
                ergoScore.OrgScore = 0;
            }


            return ergoScore;
        }
    }
}