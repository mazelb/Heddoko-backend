using DAL.Models;

namespace DAL
{
    public interface IUserRepository : IBaseRepository<User>
    {
        void SetCache(User user);

        User GetByEmailCached(string email);

        User GetByUsernameCached(string username);

        User GetByUsername(string username);

        User GetByEmail(string email);

        User GetByConfirmToken(string confirmToken);

        User GetByForgetToken(string forgetToken);
    }
}
