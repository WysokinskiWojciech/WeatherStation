using WeatherStation.Shared;
using System.Device.I2c;
using Iot.Device.Bmxx80;
using Iot.Device.Bmxx80.PowerMode;
using System;

namespace WeatherStation.Server.Service
{
    public class WeatherService: IWeatherService
    {
        private readonly Bmp280 bmp280;

        public WeatherService()
        {
            bmp280 = Initialize();
            bmp280.SetPowerMode(Bmx280PowerMode.Normal);
        }

        public Weather GetCurrentWeather()
        {
            return new Weather()
            {
                Time = DateTime.UtcNow,
                Temperature = GetTemperature(),
                Pressure = GetPressure()
            };
        }
        private static Bmp280 Initialize()
        {
            var i2cSettings = new I2cConnectionSettings(1, Bmp280.SecondaryI2cAddress);
            var i2cDevice = I2cDevice.Create(i2cSettings);
            return new Bmp280(i2cDevice);
        }

        private double GetPressure()
        {
            bmp280.TryReadPressure(out var preValue);
            return Math.Round(preValue.Hectopascals,2);
        }

        private double GetTemperature()
        {
            bmp280.TryReadTemperature(out var tempValue);
            return Math.Round(tempValue.DegreesCelsius,2);
        }
    }
}
