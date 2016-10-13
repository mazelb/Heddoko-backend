using System;
using System.Collections.Generic;
using System.Web;
using DAL.Models;

namespace Services.MailSending.Models
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
