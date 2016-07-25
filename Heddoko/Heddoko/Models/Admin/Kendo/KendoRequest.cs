using System.Collections.Generic;

namespace Heddoko.Models
{
    public class KendoRequest
    {
        public int? Take { get; set; }

        public int? Skip { get; set; }

        public int? Page { get; set; }

        public int? PageSize { get; set; }

        public List<KendoSort> Sort { get; set; }

        public KendoFilter Filter { get; set; }

        public List<KendoAggregateItem> Aggregate { get; set; }
    }
}