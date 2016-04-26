using DAL.Models;
using System.Linq;
using System.Data.Entity;

namespace DAL
{
    public class TagRepository : BaseRepository<Tag>, ITagRepository
    {
        public TagRepository(HDContext sb) : base(sb)
        {
        }
    }
}
