/**
 * @file ElmahErrorItem.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heddoko.Areas.MvcElmahDashboard.Code
{
    [Table("ELMAH_Error", Schema = "dbo")]
    public class ElmahErrorItem
    {
        public int RowNum { get; set; }

        [Column]
        public virtual int Sequence { get; set; }

        [Column]
        public virtual string Application { get; set; }

        [Column]
        public virtual DateTime TimeUtc { get; set; }
    }
}