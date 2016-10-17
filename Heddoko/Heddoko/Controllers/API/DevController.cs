using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using Services;
using System;
using System.Threading.Tasks;
using DAL.Models;
using Hangfire;

namespace Heddoko.Controllers.API
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/v1/development")]
    [AllowAnonymous]
    public class DevController : BaseAPIController
    {
        public DevController()
        {
        }

        public DevController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow)
        {
        }

        [Route("migrate-db")]
        [HttpPost]
        public IHttpActionResult Migrate()
        {
            Migrator.RunMigrations();
            return Ok();
        }

        [Route("init-db")]
        [HttpPost]
        public IHttpActionResult Init()
        {
            Migrator.InitMigration();
            return Ok();
        }

        [Route("version-db/{version}")]
        [HttpPost]
        public IHttpActionResult Version(string version)
        {
            Migrator.RunMigrations(version?.Trim());
            return Ok();
        }

        [Route("pending-db")]
        [HttpGet]
        public IHttpActionResult Pending()
        {
            return Ok(Migrator.GetPending());
        }

        [Route("flush")]
        [HttpGet]
        public IHttpActionResult Flush()
        {
            RedisManager.Flush();
            return Ok();
        }

        [Route("sendadminInvite/{id:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> SendAdminInvite(int id)
        {
            User user = UoW.UserRepository.Get(id);

            if (user.OrganizationID.HasValue)
            {
                user.Status = UserStatusType.Invited;
                UoW.Save();
                UoW.UserRepository.ClearCache(user);

                Organization organization = UoW.OrganizationRepository.GetFull(user.OrganizationID.Value);

                int organizationID = organization.Id;
                string inviteToken = await UserManager.GenerateInviteTokenAsync(user);
                UserManager.SendInviteAdminEmail(organizationID, inviteToken);
            }

            return Ok();
        }

        [Route("seed-images")]
        [HttpPost]
        public IHttpActionResult SeedImages()
        {
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Content/seed")));
            foreach (string file in files)
            {
                Azure.Upload($"seed/{Path.GetFileName(file)}", DAL.Config.AssetsContainer, file);
            }

            return Ok();
        }
    }
}