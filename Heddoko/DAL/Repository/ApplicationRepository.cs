using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Application GetFull(int id)
        {
            return DbSet.FirstOrDefault(c => c.Id == id);
        }

        public Application GetByClient(string client)
        {
            return DbSet.FirstOrDefault(c => c.Client == client);
        }

        public Application GetByClientAndSecret(string client, string secret)
        {
            return DbSet.FirstOrDefault(c => c.Client == client && c.Secret == secret);
        }

        public IEnumerable<Application> All(bool isDeleted)
        {
            return DbSet.OrderByDescending(c => c.Created);
        }

        public IEnumerable<Application> GetByUserId(int userId)
        {
            return DbSet.Where(c => c.UserID == userId)
                        .OrderByDescending(c => c.Created);
        }
    }
}