/**
 * @file ISupportEmailViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Web;
using DAL.Models;

namespace DAL.Models
{
    public interface ISupportEmailViewModel
    {
        IssueType Type { get; set; }

        IssueImportance Importance { get; set; }

        string ShortDescription { get; set; }

        string DetailedDescription { get; set; }

        IEnumerable<HttpPostedFileBase> Attachments { get; set; }

        string FullName { get; set; }

        DateTime Entered { get; set; }

        string Email { get; set; }
    }
}
