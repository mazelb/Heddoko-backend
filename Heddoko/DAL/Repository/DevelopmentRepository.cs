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

        public IEnumerable<Development> All(bool isDeleted)
        {
            return DbSet.Include(c => c.UserID)
                        .OrderByDescending(c => c.Created);
        }

        public IEnumerable<Development> GetByUserId(int userId)
        {
            return DbSet.Include(c => c.UserID)
                        .Where(c => c.UserID == userId)
                        .OrderByDescending(c => c.Created);
        }
    }
}