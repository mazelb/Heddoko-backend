/**
 * @file UserRepository.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HDContext sb)
            : base(sb)
        {
            Key = Constants.Cache.Users;
        }

        public void SetCache(User user)
        {
            SetCache(user.Email, user);
        }

        public override void ClearCache(User user)
        {
            if (user.Id <= 0)
            {
                return;
            }

            base.ClearCache(user.Id.ToString());

            if (!string.IsNullOrEmpty(user.Email))
            {
                base.ClearCache(user.Email.ToLower());
            }

            if (!string.IsNullOrEmpty(user.Token))
            {
                base.ClearCache(user.Token.ToLower());
            }

            if (!string.IsNullOrEmpty(user.UserName))
            {
                base.ClearCache(user.UserName.ToLower());
            }
        }

        public override void SetCache(string id, User user, int? hours = null)
        {
            if (user.Id <= 0)
            {
                return;
            }

            base.SetCache(user.Id.ToString(), user, hours);

            if (!string.IsNullOrEmpty(user.Email))
            {
                base.SetCache(user.Email.ToLower(), user, hours);
            }

            if (!string.IsNullOrEmpty(user.Token))
            {
                base.SetCache(user.Token.ToLower(), user, hours);
            }

            if (!string.IsNullOrEmpty(user.UserName))
            {
                base.SetCache(user.UserName.ToLower(), user, hours);
            }
        }

        public override User Get(int id)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<User> All(bool isDeleted = false)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .Where(c => isDeleted ? c.Status == UserStatusType.Deleted : c.Status != UserStatusType.Deleted)
                        .OrderBy(c => c.FirstName)
                        .ThenBy(c => c.LastName);
        }

        public override User GetFull(int id)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Tokens)
                        .Include(c => c.Team)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .Include(c => c.Devices)
                        .FirstOrDefault(c => c.Id == id);
        }

        public User GetByToken(string token)
        {
            return DbSet.Include(c => c)
                        .Include(c => c.Roles)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .FirstOrDefault(c => c.Tokens.Any(t => t.Token == token));
        }

        public User GetByEmailCached(string email)
        {
            User user = GetCached(email);
            if (user != null)
            {
                return user;
            }
            user = GetByEmail(email);
            if (user != null)
            {
                SetCache(email, user);
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public User GetByUsername(string username)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .FirstOrDefault(c => c.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public User GetByUsernameFull(string username)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Tokens)
                        .Include(c => c.Team)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .FirstOrDefault(c => c.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public User GetByUsernameCached(string username)
        {
            User user = GetCached(username);
            if (user != null)
            {
                return user;
            }

            user = GetByUsername(username);
            if (user != null)
            {
                SetCache(username, user);
            }

            return user;
        }

        public IEnumerable<User> Admins()
        {
            return DbSet.Include(c => c.Roles.Select(r => r.Role))
                        .Where(c => c.Roles.Any(u => u.Role.Name.Equals(Constants.Roles.Admin, StringComparison.OrdinalIgnoreCase)))
                        .OrderBy(c => c.FirstName)
                        .ThenBy(c => c.LastName);
        }

        public IEnumerable<User> GetByOrganization(int organizationID, bool isDeleted = false)
        {
            return DbSet.Include(c => c.Team)
                        .Include(c => c.Organization)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .Where(c => isDeleted ? c.Status == UserStatusType.Deleted : c.Status != UserStatusType.Deleted)
                        .Where(c => c.OrganizationID.Value == organizationID)
                        .OrderBy(c => c.FirstName)
                        .ThenBy(c => c.LastName);
        }

        public IEnumerable<int> GetIdsByOrganization(int organizationID, bool isDeleted = false)
        {
            return DbSet.Where(c => isDeleted ? c.Status == UserStatusType.Deleted : c.Status != UserStatusType.Deleted)
                        .Where(c => c.OrganizationID.Value == organizationID)
                        .Select(c => c.Id);
        }

        public IEnumerable<User> GetByTeam(int teamId, int organizationID, bool isDeleted = false)
        {
            if (teamId != 0)
            {
                return DbSet.Include(c => c.Team)
                            .Include(c => c.Organization)
                            .Include(c => c.Roles.Select(r => r.Role))
                            .Where(c => isDeleted ? c.Status == UserStatusType.Deleted : c.Status != UserStatusType.Deleted)
                            .Where(c => c.TeamID.Value == teamId)
                            .OrderBy(c => c.FirstName)
                            .ThenBy(c => c.LastName);
            }
            else
                return DbSet.Include(c => c.Team)
                            .Include(c => c.Organization)
                            .Include(c => c.Roles.Select(r => r.Role))
                            .Where(c => c.OrganizationID == organizationID)
                            .Where(c => isDeleted ? c.Status == UserStatusType.Deleted : c.Status != UserStatusType.Deleted)
                            .Where(c => c.TeamID == null)
                            .OrderBy(c => c.FirstName)
                            .ThenBy(c => c.LastName);
        }

        public IEnumerable<int> GetIdsByTeam(int teamID, bool isDeleted = false)
        {
            return DbSet.Where(c => isDeleted ? c.Status == UserStatusType.Deleted : c.Status != UserStatusType.Deleted)
                        .Where(c => c.TeamID.Value == teamID)
                        .Select(c => c.Id);
        }

        public IEnumerable<User> Search(
            string search,
            int? organizationID = null,
            bool isDeleted = false)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Team)
                        .Include(c => c.Roles.Select(r => r.Role))
                        .Where(c => isDeleted ? c.Status == UserStatusType.Deleted : c.Status != UserStatusType.Deleted)
                        .Where(c => !organizationID.HasValue || c.OrganizationID.Value == organizationID)
                        .Where(
                            c =>
                                (!string.IsNullOrEmpty(c.FirstName) && c.FirstName.ToLower().Contains(search.ToLower()))
                                ||
                                (!string.IsNullOrEmpty(c.LastName) && c.LastName.ToLower().Contains(search.ToLower()))
                                ||
                                (!string.IsNullOrEmpty(c.UserName) && c.UserName.ToLower().Contains(search.ToLower()))
                                || (!string.IsNullOrEmpty(c.Email) && c.Email.ToLower().Contains(search.ToLower())))
                        .OrderBy(c => c.FirstName)
                        .ThenBy(c => c.LastName);
        }

        public IEnumerable<User> GetByOrganizationAPI(int organizationID, int teamID, int take, int? skip = 0)
        {
            IQueryable<User> query = DbSet.Where(c => c.OrganizationID == organizationID
                                                   && c.TeamID == teamID)
                                          .Include(c => c.Team)
                                          .OrderBy(c => c.FirstName)
                                          .ThenBy(c => c.LastName);
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            query = query.Take(take);
            return query;
        }

        public int GetByOrganizationAPICount(int organizationID, int teamID)
        {
            return DbSet.Count(c => c.OrganizationID == organizationID
                                 && c.TeamID == teamID);
        }
    }
}