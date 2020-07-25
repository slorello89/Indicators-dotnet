using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Daany;
using Numpy;
using ScottPlot;
using ScottPlot.Config;

namespace Indicators
{
    class Program
    {
        const int WINDOW = 20;
        static void Main(string[] args)
        {
            var symbol = "TSLA";            
            var df = DataFrame.FromCsv(Path.Join("data", $"{symbol}.csv"));
            
                        
            Range rng = new Range (504,1008);
            rng = new Range(df.RowCount() - 505, df.RowCount() - 1);
            var dates = df["Date"].Select(d=>Convert.ToDateTime(d)).ToArray()[rng];
            var startDate = dates[0];
            var endDate = dates.Last();
            var close = np.array(df["Adj Close"].Select(f => Convert.ToDouble(f)).ToArray()[rng])[$"{WINDOW - 1}:"];
            var sma = SimpleMovingAverage.CalculateSma(df, rng);            
            var std2x = 2 * RollingVolatility.CalculateRollingVolatility(df, rng);
            var upper = sma + std2x;
            var lower = sma - std2x;
            var bbp = (close - lower) / (upper - lower);

            var smaRatio = close / sma;

            var plt = new Plot(600, 400);
            double[] xs = DataGen.Consecutive(bbp.size);

            plt.Title($"SMA Ratio for {symbol} {startDate.ToShortDateString()} to {endDate.ToShortDateString()}");
            plt.PlotScatter(xs, smaRatio.GetData<double>(), label: "SmaRatio", markerSize: 1);            
            plt.Legend(location: legendLocation.lowerLeft);
            plt.YLabel("Ratio");
            plt.XLabel("Day");            
            plt.SaveFig("SmaRatio.png");
            plt.Clear();

            plt.Title($"Bollinger Bands® for {symbol} {startDate.ToShortDateString()} to {endDate.ToShortDateString()}");
            plt.PlotScatter(xs, close.GetData<double>(), label: "Close", markerSize: 1);
            plt.PlotScatter(xs, upper.GetData<double>(), label: "Upper", markerSize: 1);
            plt.PlotScatter(xs, lower.GetData<double>(), label: "Lower", markerSize: 1);
            plt.YLabel("Dollars");
            plt.XLabel("Day");
            plt.Legend(location: legendLocation.upperLeft);            
            plt.SaveFig("BB.png");
            plt.Clear();

            plt.PlotScatter(xs, bbp.GetData<double>(), label: "BBP", markerSize: 1);
            plt.YLabel("Dollars");
            plt.XLabel("Day");
            plt.Legend(location: legendLocation.lowerLeft);            
            plt.SaveFig("BBP.png");
            plt.Clear();
        }
    }
}
