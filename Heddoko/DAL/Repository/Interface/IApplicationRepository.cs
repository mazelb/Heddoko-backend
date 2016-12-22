using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IApplicationRepository : IBaseRepository<Application>
    {
        Application GetByClient(string client);

        Application GetByClientAndSecret(string client, string secret);

        IEnumerable<Application> GetByUserId(int UserId);
    }
}
