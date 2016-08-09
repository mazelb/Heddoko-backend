using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Heddoko.Models
{
    public class InventoryViewModel : BaseViewModel
    {
        public List<AssembliesAPIModel> Assemblies { get; set; }
    }
}