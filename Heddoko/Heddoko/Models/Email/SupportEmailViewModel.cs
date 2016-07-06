using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
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
            get
            {
                return Entered.ToString("MM/dd/yyyy");
            }
        }
    }
}