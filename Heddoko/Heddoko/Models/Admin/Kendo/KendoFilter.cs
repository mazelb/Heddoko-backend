using Heddoko.Helpers.Kendo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
