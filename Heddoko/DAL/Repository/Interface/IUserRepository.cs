using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
    public interface IUserRepository : IBaseRepository<User>
    {
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

        IEnumerable<User> All(bool isDeleted = false);

        IEnumerable<User> GetByOrganization(int value, bool isDeleted = false, int? licenseID = null);

        IEnumerable<User> Search(string search, int? organizationID = null, bool isDeleted = false, int? licenseID = null);

        IEnumerable<User> GetByOrganizationAPI(int organizationID, int take, int? skip = 0);

        int GetByOrganizationAPICount(int organizationID);
    }
}