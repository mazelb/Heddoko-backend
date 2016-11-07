using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class DevelopmentAPIViewModel
    {
        public int? ID { get; set; }

        public string Name { get; set; }

        public string Client { get; set; }

        public string Secret { get; set; }

        public bool Enabled { get; set; }
    }
}