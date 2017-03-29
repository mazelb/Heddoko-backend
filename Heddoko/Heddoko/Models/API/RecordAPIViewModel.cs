/**
 * @file RecordAPIViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;

namespace Heddoko.Models.API
{
    public class RecordAPIViewModel
    {
        public int? KitID { get; set; }

        public string Label { get; set; }

        public int? BrainpackID { get; set; }

        public int? UserID { get; set; }

        public List<AssetFileAPIViewModel> FileTypes { get; set; }
    }
}