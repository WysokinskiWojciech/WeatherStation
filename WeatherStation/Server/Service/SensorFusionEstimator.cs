using System;
using System.Linq;

namespace WeatherStation.Server.Service
{
    public class SensorFusionEstimator
    {
        public static double Estimate(params double[] vals)
        {
            if (vals.Count() % 2 == 0)
            {
                var multiplier = 0d;
                var divider = 0d;

                for (int i = 0; i < vals.Count()/2; i++)
                {
                    multiplier += vals[2*i] * 1 / Math.Pow(vals[2*i+1], 2);
                    divider += 1 / Math.Pow(vals[2*i + 1], 2);
                }
                return Math.Round(multiplier / divider, 2);
            }
            else
                throw new Exception("Invalid number of args");
        }
    }
}