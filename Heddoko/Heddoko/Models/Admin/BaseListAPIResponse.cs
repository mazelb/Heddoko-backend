using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class BaseListAPIResponse<T> : List<T> where T : BaseAPIModel
    {

    }
}