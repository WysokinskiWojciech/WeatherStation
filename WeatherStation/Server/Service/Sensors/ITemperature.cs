using Iot.Device.Ahtxx;
using System;

namespace WeatherStation.Server.Service.Sensors
{
    public interface ITemperature
    {
        public double GetTemperature();
    }
}
