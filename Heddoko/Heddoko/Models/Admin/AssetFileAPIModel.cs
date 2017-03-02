/**
 * @file AssetFileAPIModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using DAL.Models;

namespace Heddoko.Models.Admin
{
    public class AssetFileAPIModel
    {
        public AssetType Type { get; set; }

        public string FileName { get; set; }

        public string Url { get; set; }
    }
}