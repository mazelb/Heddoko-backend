using DAL.Models;
using System.Collections.Generic;

namespace DAL
{
    public interface IPantsOctopiRepository : IBaseRepository<PantsOctopi>
    {
        IEnumerable<PantsOctopi> GetByOrganization(int organizationID);
    }
}
