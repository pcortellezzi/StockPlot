﻿using StockPlot.Charts.Models;
using StockPlot.Indicators;

namespace StockPlot.Charts
{
    public static class IndicatorsHelper
    {
        public static bool Calc(this IndicatorBase indicator, StockPricesModel model)
        {
            return indicator.Calculate(model.Prices.Count,
                model.Prices.Select(x => x.DateTime).ToArray(),
                model.Prices.Select(x => x.Open).ToArray(),
                model.Prices.Select(x => x.High).ToArray(),
                model.Prices.Select(x => x.Low).ToArray(),
                model.Prices.Select(x => x.Close).ToArray(),
                model.Prices.Select(x => x.Volume).ToArray());
        }
    }
}