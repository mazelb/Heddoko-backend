using Heddoko.Helpers.Kendo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class KendoAggregateItem
    {
        public AggregateLogic Aggregate { get; set; }
        public string Field { get; set; }
    }
}