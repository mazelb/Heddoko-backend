/**
 * @file KendoResponse.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
namespace Heddoko.Models
{
    public class KendoResponse<T>
    {
        public T Response { get; set; }

        public int? Total { get; set; }

        public object Aggregates { get; set; }
    }
}