using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class FirmwareRepository : BaseRepository<Firmware>, IFirmwareRepository
    {
        public FirmwareRepository(HDContext sb)
            : base(sb)
        {
        }

        public override Firmware GetFull(int id)
        {
            return DbSet.Include(c => c.Asset)
                        .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Firmware> All(bool isDeleted)
        {
            return DbSet.Include(c => c.Asset)
                        .Where(c => isDeleted ? c.Status == FirmwareStatusType.Inactive : c.Status != FirmwareStatusType.Inactive)
                        .OrderByDescending(c => c.Created);
        }

        public IEnumerable<Firmware> GetByType(FirmwareType type, int? take = null, int? skip = null)
        {
            IQueryable<Firmware> query = DbSet.Include(c => c.Asset)
                                  .Where(c => c.Status == FirmwareStatusType.Active)
                                  .Where(c => c.Type == type)
                                  .OrderByDescending(c => c.Created);


            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public IEnumerable<Firmware> Search(string search, bool isDeleted = false)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.Asset)
                        .Where(c => isDeleted ? c.Status == FirmwareStatusType.Inactive : c.Status != FirmwareStatusType.Inactive)
                        .Where(c => (c.Id == id)
                                    || c.Version.ToString().ToLower().Contains(search.ToLower()))
                        .OrderByDescending(c => c.Created);
        }


        public Firmware LastFirmwareByType(FirmwareType type)
        {
            return DbSet.Include(c => c.Asset)
                        .OrderByDescending(c => c.Created)
                        .FirstOrDefault(c => c.Type == type);
        }

        public int GetCountByType(FirmwareType type)
        {
            return DbSet.Count(c => c.Status == FirmwareStatusType.Active && c.Type == type);
        }
    }
}