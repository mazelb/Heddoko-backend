using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class ComponentsAPIModel : BaseAPIModel
    {
        public EquipmentStatusType Status { get; set; }

        public int Quantity { get; set; }

        public ComponentsType Type { get; set; }
    }
}