/**
 * @file Results.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        // TODO - BENB - Standardizes error codes/ should probably be an enum
        public int ErrorCode { get; set; }
        public Result()
        {
            Success = false;
            Message = "";
            ErrorCode = 500;
        }
    }

    public class GetOneResult<TEntity> : Result where TEntity : class, new()
    {
        public TEntity Entity { get; set; }
    }

    public class GetManyResults<TEntity> : Result where TEntity : class, new()
    {
        public IEnumerable<TEntity> Entities { get; set; }
    }

    public class GetListResult<T> : Result
    {
        public List<T> Entities { get; set; }
    }
}
