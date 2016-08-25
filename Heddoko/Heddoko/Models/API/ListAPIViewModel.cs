using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class ListAPIViewModel<T>
    {
        public int TotalCount { get; set; }

        public List<T> Collection { get; set; }
    }
}