using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System;

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
                        .Where(c => c.Email == email).FirstOrDefault();
        }

        public User GetByUsername(string username)
        {
            return DbSet.Include(c => c.Asset)
                        .Where(c => c.Username == username).FirstOrDefault();
        }

        public User GetByConfirmToken(string confirmToken)
        {
            return DbSet.Where(c => c.ConfirmToken == confirmToken).FirstOrDefault();
        }

        public User GetByForgetToken(string forgetToken)
        {
            return DbSet.Where(c => c.ForgotToken == forgetToken).FirstOrDefault();
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
    }
}
