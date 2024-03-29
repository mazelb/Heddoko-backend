﻿/**
 * @file DevController.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
using DAL.Models.Enum;
using Hangfire;
using Heddoko.Helpers.DomainRouting.Http;
using Microsoft.AspNet.Identity.Owin;

namespace Heddoko.Controllers.API
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix("api/v1/development")]
    [AllowAnonymous]
    public class DevController : ApiController
    {
        [DomainRoute("notification", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public IHttpActionResult Test()
        {
            PushService.SendDesktopNotification("yo", "token", UserEventType.StreamChannelClosed, "1");
            return Ok();
        }

        [DomainRoute("flush", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public IHttpActionResult Flush()
        {
            RedisManager.Flush();
            return Ok();
        }

        [DomainRoute("sendadminInvite/{id:int}", Constants.ConfigKeyName.DashboardSite)]
        [HttpGet]
        public async Task<IHttpActionResult> SendAdminInvite(int id)
        {
            UnitOfWork uow = new UnitOfWork(new HDContext());
            User user = uow.UserRepository.Get(id);

            if (user.OrganizationID.HasValue)
            {
                user.Status = UserStatusType.Invited;
                uow.Save();
                uow.UserRepository.ClearCache(user);

                Organization organization = uow.OrganizationRepository.GetFull(user.OrganizationID.Value);

                int organizationID = organization.Id;
                var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

                string inviteToken = await userManager.GenerateInviteTokenAsync(user);
                userManager.SendInviteAdminEmail(organizationID, inviteToken);
            }

            return Ok();
        }

        [DomainRoute("seed-images", Constants.ConfigKeyName.DashboardSite)]
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