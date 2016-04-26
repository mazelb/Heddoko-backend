using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
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
            if (!string.IsNullOrEmpty(user.Email))
            {
                base.SetCache(user.Email, user);
            }


            if (user.ID > 0)
            {
                base.SetCache(user.ID.ToString(), user);
            }

            if (!string.IsNullOrEmpty(user.Token))
            {
                base.SetCache(user.Token, user);
            }
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
            return DbSet.Where(c => c.Email == email).FirstOrDefault();
        }

        public User GetByConfirmToken(string confirmToken)
        {
            return DbSet.Where(c => c.ConfirmToken == confirmToken).FirstOrDefault();
        }

        public User GetByForgetToken(string forgetToken)
        {
            return DbSet.Where(c => c.ForgotToken == forgetToken).FirstOrDefault();
        }
    }
}
