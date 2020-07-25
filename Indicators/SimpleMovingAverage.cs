using Daany;
using Numpy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indicators
{
    public class SimpleMovingAverage
    {
        const int WINDOW = 20;
        public static NDarray CalculateSma(DataFrame df, Range rng)
        {
            return np.array(df.Rolling(WINDOW, Aggregation.Avg)["Adj Close"].Select(f => Convert.ToDouble(f)).ToArray()[rng])[$"{WINDOW - 1}:"];
        }
    }
}
