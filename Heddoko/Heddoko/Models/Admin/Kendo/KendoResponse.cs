using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class KendoResponse<T>
    {
        public T Response { get; set; }

        public int? Total { get; set; }

        public object Aggregates { get; set; }
    }
}