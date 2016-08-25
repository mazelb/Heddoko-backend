using DAL.Models;

namespace DAL
{
    public class AssetManager
    {
        public static string Path(string name, AssetType type)
        {
            return $"{type.GetStringValue()}/{name}";
        }
    }
}
