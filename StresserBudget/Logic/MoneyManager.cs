using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public static class MoneyManager
    {
        public static decimal GetFrankenRappen(decimal aRappen, bool aRoundUp)
        {
            decimal lFranken = aRappen / 100;

            if (aRoundUp)
            {
                lFranken = Math.Ceiling(lFranken * 20) / 20;
            }
            return lFranken;
        }

        public static decimal GetFrankenRappen(double aRappen, bool aRoundUp)
        {
            return GetFrankenRappen(Convert.ToDecimal(aRappen), aRoundUp);
        }

        public static decimal GetFrankenRappen(int aRappen, bool aRoundUp)
        {
            return GetFrankenRappen(Convert.ToDecimal(aRappen), aRoundUp);
        }
    }
}
