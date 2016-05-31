using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HDContext sb) : base(sb)
        {
            Key = Constants.Cache.Users;
        }

        public void SetCache(User user)
        {
            SetCache(user.Email, user);
        }

        public override void SetCache(string id, User user)
        {

            if (user.ID > 0)
            {
                base.SetCache(user.ID.ToString(), user);

                if (!string.IsNullOrEmpty(user.Email))
                {
                    base.SetCache(user.Email, user);
                }

                if (!string.IsNullOrEmpty(user.Token))
                {
                    base.SetCache(user.Token, user);
                }

                if (!string.IsNullOrEmpty(user.Username))
                {
                    base.SetCache(user.Username, user);
                }
            }
        }

        public override User Get(int id)
        {
            return DbSet.Include(c => c.Asset)
                        .Include(c => c.Organization)
                        .Include(c => c.License)
                        .FirstOrDefault(c => c.ID == id);
        }

        public User GetFull(int id)
        {
            return DbSet.Include(c => c.Asset)
                        .Include(c => c.Tokens)
                        .Include(c => c.Organization)
                        .Include(c => c.License)
                        .FirstOrDefault(c => c.ID == id);
        }

        public User GetByEmailCached(string email)
        {
            User user = GetCached(email);
            if (user == null)
            {
                user = GetByEmail(email);
                if (user != null)
                {
                    SetCache(email, user);
                }
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            return DbSet.Include(c => c.Asset)
                        .Include(c => c.Organization)
                        .Where(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return DbSet.Include(c => c.Asset)
                        .Include(c => c.Organization)
                        .Where(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public User GetByUsernameFull(string username)
        {
            return DbSet.Include(c => c.Asset)
                        .Include(c => c.Organization)
                        .Include(c => c.Tokens)
                        .Where(c => c.Username.Equals(username, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();
        }

        public User GetByConfirmToken(string confirmToken)
        {
            return DbSet.Where(c => c.ConfirmToken.Equals(confirmToken, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public User GetByForgetToken(string forgetToken)
        {
            return DbSet.Where(c => c.ForgotToken.Equals(forgetToken, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public User GetByUsernameCached(string username)
        {
            User user = GetCached(username);
            if (user == null)
            {
                user = GetByUsername(username);
                if (user != null)
                {
                    SetCache(username, user);
                }
            }

            return user;
        }

        public IEnumerable<User> Admins()
        {
            return DbSet.Where(c => c.Role == UserRoleType.Admin)
                        .OrderBy(c => c.FirstName)
                        .OrderBy(c => c.LastName);
        }

        public User GetByInviteToken(string inviteToken)
        {
            return DbSet.Include(c => c.Organization)
                        .Where(c => c.InviteToken.Equals(inviteToken, StringComparison.OrdinalIgnoreCase))
                        .FirstOrDefault();
        }

        public IEnumerable<User> GetByOrganization(int organizationID)
        {
            return DbSet.Include(c => c.License)
                        .Include(c => c.Organization)
                        .Where(c => c.OrganizationID.Value == organizationID)
                        .OrderBy(c => c.FirstName)
                        .OrderBy(c => c.LastName);
        }

        public IEnumerable<User> Search(string search, int? organizationID = null)
        {
            return DbSet.Include(c => c.License)
                        .Include(c => c.Organization)
                        .Where(c => organizationID.HasValue ? c.OrganizationID.Value == organizationID : true)
                        .AsEnumerable()
                        .Where(c => c.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                 || c.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)
                                 || c.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                        .OrderBy(c => c.FirstName)
                        .OrderBy(c => c.LastName);
        }
    }
}
