﻿/**
 * @file KendoSort.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using Heddoko.Helpers.Kendo;

namespace Heddoko.Models
{
    public class KendoSort
    {
        public SortDirection Dir { get; set; }
        public string Field { get; set; }
    }
}