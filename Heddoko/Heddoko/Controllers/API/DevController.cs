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
    public class DevController : ApiController
    {
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
        public IHttpActionResult SendAdminInvite(int id)
        {
            UnitOfWork uow = new UnitOfWork(new HDContext());

            User user = uow.UserRepository.Get(id);

            if (user.OrganizationID.HasValue)
            {
                user.Status = UserStatusType.Invited;
                user.InviteToken = PasswordHasher.Md5(DateTime.Now.Ticks.ToString());
                uow.Save();
                uow.UserRepository.ClearCache(user);

                Organization organization = uow.OrganizationRepository.GetFull(user.OrganizationID.Value);

                int organizationID = organization.Id;
                BackgroundJob.Enqueue(() => EmailManager.SendInviteAdminEmail(organizationID));
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