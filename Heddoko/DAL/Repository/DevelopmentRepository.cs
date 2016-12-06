using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class DevelopmentRepository : BaseRepository<Development>, IDevelopmentRepository
    {
        public DevelopmentRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Development GetFull(int id)
        {
            return DbSet.FirstOrDefault(c => c.Id == id);
        }

        public Development GetByClient(string client, string secret)
        {
            return DbSet.FirstOrDefault(c => c.Client == client && c.Secret == secret);
        }

        public IEnumerable<Development> All(bool isDeleted)
        {
            return DbSet.OrderByDescending(c => c.Created);
        }

        public IEnumerable<Development> GetByUserId(int userId)
        {
            return DbSet.Where(c => c.UserID == userId)
                        .OrderByDescending(c => c.Created);
        }
    }
}