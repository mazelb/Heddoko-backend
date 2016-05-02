using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class UserAPIModel : BaseAPIModel
    {
        public string Name { get; set; }
    }
}