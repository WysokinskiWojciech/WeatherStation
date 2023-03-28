using Iot.Device.Ahtxx;
using System;

namespace WeatherStation.Server.Service.Sensors
{
    public class Aht20Sensor : Sensor, IPrecision,ITemperature
    {
        private readonly Aht20 aht20;
        public double Precision => 0.3;
        public Aht20Sensor()
        {
            aht20 = new Aht20(Initialize(Aht20.DefaultI2cAddress)); 
        }
        public double GetHumidity()
        {
            return Math.Round(aht20.GetHumidity().Percent, 2);
        }
        public double GetTemperature()
        {
            return Math.Round(aht20.GetTemperature().DegreesCelsius, 2);
        }
    }
}
