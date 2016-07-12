using DAL.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace DAL
{
    public class PantsOctopiRepository : BaseRepository<PantsOctopi>, IPantsOctopiRepository
    {
        public PantsOctopiRepository(HDContext sb) : base(sb)
        {
        }

        public IEnumerable<PantsOctopi> GetByOrganization(int organizationID)
        {
            return All().Where(c => c.OrganizationID == organizationID);
        }
    }
}
