/**
 * @file KendoFilterItem.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using Heddoko.Helpers.Kendo;

namespace Heddoko.Models
{
    public class KendoFilterItem
    {
        public FilterOperator Operator { get; set; }

        public string Field { get; set; }

        public string Value { get; set; }
    }
}