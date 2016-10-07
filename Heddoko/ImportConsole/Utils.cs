using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportConsole
{
    static class Utils
    {
        public static decimal? ToNullableDecimal(this string s)
        {
            decimal i;
            if (Decimal.TryParse(s, out i))
            {
                return i;
            }
            return null;
        }

        public static int? ToNullableInt(this string s)
        {
            int i;
            if (Int32.TryParse(s, out i))
            {
                return i;
            }
            return null;
        }
    }
}
