using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class FolderRepository : BaseRepository<Folder>, IFolderRepository
    {
        public FolderRepository(HDContext sb) : base(sb)
        {
        }
    }
}
