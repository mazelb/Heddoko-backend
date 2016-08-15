using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using DAL.Models;

namespace Heddoko.Models
{
    public class InventoryViewModel : BaseViewModel
    {
        public List<Assembly> Assemblies { get; set; }
    }
}