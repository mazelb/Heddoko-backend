using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL
{
    public interface IUserRepository : IBaseRepository<User>
    {
        void SetCache(User user);

        User GetByEmailCached(string email);

        User GetByEmail(string email);

        User GetByConfirmToken(string confirmToken);

        User GetByForgetToken(string forgetToken);
    }
}
