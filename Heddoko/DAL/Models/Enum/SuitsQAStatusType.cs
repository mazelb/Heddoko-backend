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
        BaseplateFailed = 2,
        WiringTested = 3,
        WiringFailed = 4,
        ConnectorTested = 5,
        ConnectorFailed = 6,
        PowerTested = 7,
        PowerFailed = 7
    }
}
