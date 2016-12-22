using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using i18n;

namespace Heddoko.Models
{
    public class AuthorizeViewModel : BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Client { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ClientSecret { get; set; }
    }
}