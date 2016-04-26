using DAL.Models;

namespace DAL
{
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        Asset GetByImage(string name);
    }
}
