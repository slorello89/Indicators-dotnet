using Numpy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Indicators
{
    public class DailyReturns 
    {
        public static NDarray CalculateDailyReturns(NDarray input)
        {
            var rolled = np.roll(input, new int[] { 1 });
            return input - rolled;
        }
    }
}
