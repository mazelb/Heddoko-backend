using DAL;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

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
            DAL.Migrator.RunMigrations();
            return Ok();
        }

        [Route("init-db")]
        [HttpPost]
        public IHttpActionResult Init()
        {
            DAL.Migrator.InitMigration();
            return Ok();
        }

        [Route("version-db/{version}")]
        [HttpPost]
        public IHttpActionResult Version(string version)
        {
            DAL.Migrator.RunMigrations(version?.Trim());
            return Ok();
        }

        [Route("pending-db")]
        [HttpGet]
        public IHttpActionResult Pending()
        {
            return Ok(DAL.Migrator.GetPending());
        }

        [Route("flush")]
        [HttpGet]
        public IHttpActionResult Flush()
        {
            RedisManager.Flush();
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
                Azure.Upload($"seed/{Path.GetFileName(file)}", Config.AssetsContainer, file);
            }

            return Ok();
        }
    }
}