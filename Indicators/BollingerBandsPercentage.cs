using Numpy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Indicators
{
    public class BollingerBands
    {
        public static NDarray CalculateBBP(NDarray sma, NDarray rollingStd, NDarray close)
        {
            var upper = sma + (rollingStd * 2);
            var lower = sma - (rollingStd * 2);
            var bbp = (close - lower) / (upper - lower);
            return bbp;
        }

        public static (NDarray, NDarray) CalculateBollingerBands(NDarray sma, NDarray rollingStd)
        {
            var upper = sma + (rollingStd * 2);
            var lower = sma - (rollingStd * 2);
            return (upper, lower);
        }
    }
}
