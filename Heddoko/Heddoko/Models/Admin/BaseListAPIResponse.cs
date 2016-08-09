using System.Collections.Generic;

namespace Heddoko.Models
{
    public class BaseListAPIResponse<T> : List<T> where T : BaseAPIModel
    {
    }
}