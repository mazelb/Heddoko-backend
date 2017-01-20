/**
 * @file KendoFilter.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;
using System.Linq;
using Heddoko.Helpers.Kendo;

namespace Heddoko.Models
{
    public class KendoFilter
    {
        public List<KendoFilterItem> Filters { get; set; }
        public FilterLogic Logic { get; set; }

        public KendoFilterItem Get(string key)
        {
            return Filters != null ? Filters.FirstOrDefault(c => c.Field.Equals(key)) : null;
        }
    }
}