using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class ApplicationAPIViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Client { get; set; }

        public string Secret { get; set; }

        public string RedirectUrl { get; set; }

        public bool Enabled { get; set; }
    }
}