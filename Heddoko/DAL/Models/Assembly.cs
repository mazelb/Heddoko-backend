using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Assembly : BaseModel
    {
        public Assembly(AssembliesType assembly, int onHand, int producible)
        {
            Type = assembly;
            QuantityOnHand = onHand;
            QuantityProducible = producible;
        }

        public AssembliesType Type { get; set; }

        public int QuantityOnHand { get; set; }

        public int QuantityProducible { get; set; }
    }
}
