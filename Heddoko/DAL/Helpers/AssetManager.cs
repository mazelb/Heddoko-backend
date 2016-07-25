using DAL.Models;

namespace DAL
{
    public class AssetManager
    {
        public static string ImagePath(string name, AssetType type)
        {
            return $"{type.GetStringValue()}{name}";
        }
    }
}
