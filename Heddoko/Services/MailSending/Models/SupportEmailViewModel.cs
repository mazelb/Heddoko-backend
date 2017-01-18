/**
 * @file SupportEmailViewModel.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 12 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Web;
using DAL.Models;

namespace Services.MailSending.Models
{
    public class SupportEmailViewModel : EmailViewModel
    {
        public IssueType Type { get; set; }

        public IssueImportance Importance { get; set; }

        public string ShortDescription { get; set; }

        public string DetailedDescription { get; set; }

        public IEnumerable<HttpPostedFileBase> Attachments { get; set; }

        public string FullName { get; set; }

        public DateTime Entered { get; set; }

        public string EnteredView
        {
            get { return Entered.ToString("MM/dd/yyyy"); }
        }
    }
}