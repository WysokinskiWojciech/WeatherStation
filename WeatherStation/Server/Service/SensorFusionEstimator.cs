using System;

namespace WeatherStation.Server.Service
{
    public class SensorFusionEstimator
    {
        public static double Estimate(double value1, double precision1, double value2, double precision2)
        {
            var multiplier = value1 * 1 / Math.Pow(precision1, 2) + value2 * 1 / Math.Pow(precision2, 2);
            var divider = 1 / Math.Pow(precision1, 2) + 1 / Math.Pow(precision2, 2);
            return Math.Round(multiplier/divider,2);
        }
    }
}