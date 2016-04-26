using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
