using DAL.Models;

namespace DAL
{
    public interface IAccessTokenRepository : IBaseRepository<AccessToken>
    {
        AccessToken GetByToken(string token);
    }
}
