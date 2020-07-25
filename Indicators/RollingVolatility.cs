using Daany;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indicators
{
    public class RollingVolatility
    {
        const int WINDOW = 20;
        public static NDarray CalculateRollingVolatility(DataFrame df, Range rng)
        {
            return np.array(df.Rolling(WINDOW, Aggregation.Std)["Adj Close"].Select(f => Convert.ToDouble(f)).ToArray()[rng])[$"{WINDOW - 1}:"];
        }
    }
}
