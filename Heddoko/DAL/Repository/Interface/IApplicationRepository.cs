using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IApplicationRepository : IBaseRepository<Application>
    {
        IEnumerable<Application> All(bool isDeleted);

        Application GetByClient(string client);

        Application GetByClientAndSecret(string client, string secret);

        IEnumerable<Application> GetByUserId(int UserId);
    }
}
