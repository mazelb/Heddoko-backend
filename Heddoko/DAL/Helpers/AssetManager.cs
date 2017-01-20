/**
 * @file AssetManager.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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
