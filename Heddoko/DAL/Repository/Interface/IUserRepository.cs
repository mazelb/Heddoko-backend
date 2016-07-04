using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetFull(int id);

        void SetCache(User user);

        void ClearCache(User user);

        User GetByEmailCached(string email);

        User GetByUsernameCached(string username);

        User GetByUsername(string username);

        User GetByUsernameFull(string username);

        User GetByEmail(string email);

        User GetByConfirmToken(string confirmToken);

        User GetByInviteToken(string inviteToken);

        User GetByForgetToken(string forgetToken);

        IEnumerable<User> Admins();

        IEnumerable<User> GetByOrganization(int value);

        IEnumerable<User> Search(string search, int? organizationID = null);
    }
}
