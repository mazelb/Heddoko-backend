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
    public static class ErgoScoreService
    {
        private static UnitOfWork UoW = new UnitOfWork();
        private static User CurrentUser = GetCurrentUser();

        public static ErgoScore Get(int? id = null)
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

        public static List<ErgoScore> GetOrgScores(int orgId)
        {
            Organization org = UoW.OrganizationRepository.Get(orgId);
            IEnumerable<int> ids = org.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        public static List<ErgoScore> GetTeamScores(int teamId)
        {
            Team team = UoW.TeamRepository.Get(teamId);
            IEnumerable<int> ids = team.Users.Select(x => x.Id).Distinct();

            return UoW.AnalysisFrameRepository.GetMultipleUserScores(ids.ToArray());
        }

        public static ErgoScore GetCurrentOrgScore()
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
        public static ErgoScore GetCurrentTeamScore(int teamId)
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

        private static User GetCurrentUser()
        {
            int id = 0;
            var claimsIdentity = System.Web.HttpContext.Current.User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    id = Convert.ToInt32(userIdClaim.Value);
                }
            }

            User user = UoW.UserRepository.Get(id);

            return user;
        }
    }
}
