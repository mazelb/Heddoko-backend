using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IApplicationRepository : IBaseRepository<Application>
    {
        IEnumerable<Application> All(int? take = null, int? skip = null);

        Application GetByClient(string client);

        Application GetByClientAndSecret(string client, string secret);

        IEnumerable<Application> GetByUserId(int userId, int? take = null, int? skip = null);
    }
}
