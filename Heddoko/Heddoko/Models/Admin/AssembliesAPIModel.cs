using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;
using i18n;

namespace Heddoko.Models
{
    public class AssembliesAPIModel
    {
        public AssembliesAPIModel()
        {
        }

        public AssembliesAPIModel(AssembliesType assembly, int onHand, int producible)
        {
            Assembly = assembly;
            QuantityOnHand = onHand;
            QuantityProducible = producible;
        }

        public AssembliesType Assembly { get; set; }

        public int QuantityOnHand { get; set; }

        public int QuantityProducible { get; set; }
    }
}