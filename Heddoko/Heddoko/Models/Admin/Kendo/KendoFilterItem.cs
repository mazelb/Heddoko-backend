using Heddoko.Helpers.Kendo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class KendoFilterItem
    {
        public FilterOperator Operator { get; set; }

        public string Field { get; set; }

        public string Value { get; set; }
    }
}