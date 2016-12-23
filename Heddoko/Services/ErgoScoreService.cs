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
        private static UnitOfWork UoW = new UnitOfWork();
        private static User CurrentUser;

        public ErgoScoreService()
        {
            CurrentUser = GetCurrentUser();
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
            Organization org = UoW.OrganizationRepository.Get(orgId);
            IEnumerable<int> ids = org.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        public List<ErgoScore> GetTeamScores(int teamId)
        {
            Team team = UoW.TeamRepository.Get(teamId);
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
        public ErgoScore GetCurrentTeamScore(int teamId)
        {
            ErgoScore ergoScore = new ErgoScore();

            Team team = UoW.TeamRepository.Get(teamId);
            if (team.Users != null)
            {
                IEnumerable<int> users = team.Users.Select(x => x.Id).ToList();
                ergoScore.Score = UoW.AnalysisFrameRepository.GetTeamScore(users.ToArray());
                ergoScore.Id = teamId;
            }

            return ergoScore;
        }

        private User GetCurrentUser()
        {
            int id = HttpContext.Current.User.Identity.GetUserId<int>();

            User user = UoW.UserRepository.Get(id);

            return user;
        }
    }
}
