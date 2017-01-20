/**
 * @file Assembly.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Assembly : BaseModel
    {
        public Assembly()
        {

        }

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
