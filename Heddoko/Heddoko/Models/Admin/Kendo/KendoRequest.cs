/**
 * @file KendoRequest.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
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