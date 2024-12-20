﻿using ScottPlot;

namespace StockPlot.Indicators.Indicators
{
    public sealed class CCI : IndicatorBase
    {
        [IndicatorParameter]
        public int Period { get; set; } = 20;

        public CCI()
        {

            IsExternal = true;

            CreateLevel(0, Colors.White);
            CreateLevel(100, Colors.Gray);
            CreateLevel(-100, Colors.Gray);
        }

        public override void Init()
        {
            Name = $"CCI [{Period}]";
        }

        public XYSerie Cci { get; private set; } = new XYSerie("CCI") { DefaultColor = Colors.BlueViolet };

        protected override void Calculate_(int total, DateTime[] time, double[] open, double[] high, double[] low, double[] close)
        {
            var meanDeviationList = new double[total];

            for (int i = 0; i < total; i++)
            {
                if (i >= Period - 1)
                {
                    meanDeviationList[i] = close.GetSMA(i, Period);
                    var std = close.GetStdDev(i, Period, meanDeviationList);

                    double cci = (close[i] - meanDeviationList[i]) / (0.015 * std);
                    Cci.Append((time[i], cci));
                }
                else
                {
                    meanDeviationList[i] = 0.0;
                    Cci.Append((time[i], double.NaN));
                }
            }
        }
    }
}
