/**
 * @file ErgoScoreService.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Resources;
using AutoMapper;
using DAL;
using DAL.Models;
using i18n;
using System.Security.Claims;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Services
{
    public class ErgoScoreService
    {
        private static UnitOfWork UoW;
        private static User CurrentUser;

        public ErgoScoreService(UnitOfWork uow, User user)
        {
            UoW = uow;
            CurrentUser = user;
        }      

        public ErgoScore Get(int? id = null)
        {
            if (!id.HasValue)
            {                
                id = CurrentUser.Id;
            }

            ErgoScore ergoScore = new ErgoScore();

            ergoScore.Score = UoW.AnalysisFrameRepository.GetUserScore(id.Value);
            ergoScore.Id = id.Value;

            return ergoScore;
        }

        public List<ErgoScore> GetOrgScores(int orgId)
        {
            IEnumerable<int> ids = UoW.UserRepository.GetIdsByOrganization(orgId);

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        public List<ErgoScore> GetTeamScores(int teamId)
        {
            Team team = UoW.TeamRepository.GetFull(teamId);
            IEnumerable<int> ids = team.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

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
        public ErgoScore GetCurrentTeamScore()
        {
            ErgoScore ergoScore = new ErgoScore();

            Team team = UoW.TeamRepository.Get(CurrentUser.TeamID.Value);
            if (team.Users != null)
            {
                IEnumerable<int> users = team.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.Id = CurrentUser.TeamID.Value;
            }

            return ergoScore;
        }
    }
}
