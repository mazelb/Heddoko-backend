/**
 * @file BaseListAPIResponse.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System.Collections.Generic;

namespace Heddoko.Models
{
    public class BaseListAPIResponse<T> : List<T> where T : BaseAPIModel
    {
    }
}