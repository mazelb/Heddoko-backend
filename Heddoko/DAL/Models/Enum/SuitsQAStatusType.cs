using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum SuitsQAStatusType
    {
        FullTested = 0,
        BaseplateTested = 1,
        WiringTested = 2,
        ConnectorTested = 3,
        PowerTested = 3
    }
}
