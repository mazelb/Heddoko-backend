using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IDevelopmentRepository : IBaseRepository<Development>
    {
        IEnumerable<Development> All(bool isDeleted);

        Development GetByClient(string client, string secret);

        IEnumerable<Development> GetByUserId(int UserId);
    }
}
