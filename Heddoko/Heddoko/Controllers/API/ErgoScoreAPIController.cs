﻿using System;
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
    [RoutePrefix("admin/api/ergoscore")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErgoScoreAPIController : BaseAPIController
    {
        public ErgoScoreAPIController() { }

        public ErgoScoreAPIController(ApplicationUserManager userManager, UnitOfWork uow) : base(userManager, uow) { }

        [Route("{id:int}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore Get(int? id = null)
        {
            ErgoScore ergoScore = new ErgoScore();

            ergoScore.Score = UoW.AnalysisFrameRepository.GetUserScore(id.Value);
            ergoScore.Id = id.Value;

            return ergoScore;
        }

        [Route("org/{id:int}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.LicenseUniversal)]
        public List<ErgoScore> GetOrgScores(int orgId)
        {
            Organization org = UoW.OrganizationRepository.Get(orgId);
            IEnumerable<int> ids = org.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        [Route("team/{id:int?}")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.AnalystAndAdmin)]
        public List<ErgoScore> GetTeamScores(int teamId)
        {
            Team team = UoW.TeamRepository.Get(teamId);
            IEnumerable<int> ids = team.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        [Route("orgScore")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore GetCurrentOrgScore()
        {
            ErgoScore ergoScore = new ErgoScore();

            Organization org = UoW.OrganizationRepository.Get(CurrentUser.OrganizationID.Value);
            if (org.Users != null)
            {
                IEnumerable<int> users = org.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.Id = org.Id;
            }

            return ergoScore;
        }

        [Route("teamScore")]
        [HttpGet]
        [AuthAPI(Roles = Constants.Roles.All)]
        public ErgoScore GetCurrentTeamScore(int teamId)
        {
            ErgoScore ergoScore = new ErgoScore();

            Team team = UoW.TeamRepository.Get(teamId);
            if(team.Users != null)
            {
                IEnumerable<int> users = team.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.Id = teamId;
            }

            return ergoScore;
        }
    }
}