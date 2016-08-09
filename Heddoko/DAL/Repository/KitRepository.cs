using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using EntityFramework.Extensions;
using System.Data.Entity;

namespace DAL
{
    public class KitRepository : BaseRepository<Kit>, IKitRepository
    {
        public KitRepository(HDContext sb)
            : base(sb)
        {
        }


        public void RemoveBrainpack(int brainpackID)
        {
            DbSet.Where(c => c.BrainpackID.Value == brainpackID).Update(c => new Kit()
            {
                BrainpackID = null
            });
        }

        public void RemoveShirt(int shirtID)
        {
            DbSet.Where(c => c.ShirtID.Value == shirtID).Update(c => new Kit()
            {
                ShirtID = null
            });
        }

        public void RemovePants(int pantsID)
        {
            DbSet.Where(c => c.PantsID.Value == pantsID).Update(c => new Kit()
            {
                PantsID = null
            });
        }

        public void RemoveSensorSet(int sensorSetID)
        {
            DbSet.Where(c => c.SensorSetID.Value == sensorSetID).Update(c => new Kit()
            {
                SensorSetID = null
            });
        }

        public override Kit GetFull(int id)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Brainpack)
                        .Include(c => c.SensorSet)
                        .Include(c => c.Pants)
                        .Include(c => c.Shirt)
                        .Include(c => c.User)
                        .FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Kit> GetAvailable(int? id = null, int? organizationID = null)
        {
            return DbSet.Where(c => c.Status != EquipmentStatusType.Trash)
                        .Where(c => c.User == null || c.UserID == id)
                        .Where(c => !organizationID.HasValue || c.OrganizationID == organizationID)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Kit> All(bool isDeleted, int? organizationID = null)
        {
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Brainpack)
                        .Include(c => c.SensorSet)
                        .Include(c => c.Pants)
                        .Include(c => c.Shirt)
                        .Include(c => c.User)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => !organizationID.HasValue || c.OrganizationID == organizationID)
                        .OrderBy(c => c.ID);
        }

        public IEnumerable<Kit> Search(string search, bool isDeleted = false, int? organizationID = null)
        {
            int? id = search.ParseID();
            return DbSet.Include(c => c.Organization)
                        .Include(c => c.Brainpack)
                        .Include(c => c.SensorSet)
                        .Include(c => c.Pants)
                        .Include(c => c.Shirt)
                        .Include(c => c.User)
                        .Where(c => isDeleted ? c.Status == EquipmentStatusType.Trash : c.Status != EquipmentStatusType.Trash)
                        .Where(c => !organizationID.HasValue || c.OrganizationID == organizationID)
                        .Where(c => (c.ID == id)
                                    || c.Location.ToLower().Contains(search.ToLower()))
                        .OrderBy(c => c.ID);
        }

        public void RemoveUser(int userID)
        {
            DbSet.Where(c => c.UserID.Value == userID).Update(c => new Kit()
            {
                UserID = null
            });
        }

        public int GetNumReady()
        {
            IEnumerable<Kit> shirts = DbSet.Where(c => c.Status == EquipmentStatusType.Ready);

            if (shirts != null)
            {
                return shirts.Count();
            }

            return 0;
        }
    }
}