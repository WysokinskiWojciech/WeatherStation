using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using System;

namespace WeatherStation.Server.Service.Sensors
{
    public class Bmp280Sensor : Sensor, IPrecision, ITemperature
    {
        private Bmp280 bmp280;
        public double Precision => 1;

        public Bmp280Sensor()
        {
            bmp280 = new Bmp280(Initialize(Bmp280.DefaultI2cAddress));
            bmp280.SetPowerMode(Bmx280PowerMode.Normal);
            bmp280.FilterMode = Iot.Device.Bmxx80.FilteringMode.Bmx280FilteringMode.X16;
            bmp280.TemperatureSampling = Sampling.UltraHighResolution;
        }
        public double GetPressure()
        {
            bmp280.TryReadPressure(out var preValue);
            return Math.Round(preValue.Hectopascals, 2);
        }

        public double GetTemperature()
        {
            bmp280.TryReadTemperature(out var tempValue);
            return Math.Round(tempValue.DegreesCelsius, 2);
        }
    }
}
