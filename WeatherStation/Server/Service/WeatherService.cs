using WeatherStation.Shared;
using System;
using WeatherStation.Server.Service.Sensors;

namespace WeatherStation.Server.Service
{
    public class WeatherService : IWeatherService
    {
        private readonly Bmp280Sensor bmp280Sensor;
        private readonly Aht20Sensor aht20Sensor;

        public WeatherService()
        {
            bmp280Sensor = new Bmp280Sensor();
            aht20Sensor = new Aht20Sensor();
        }

        public Weather GetCurrentWeather()
        {
            var bmp280Temp = bmp280Sensor.GetTemperature();
            var aht20Temp = aht20Sensor.GetTemperature();
            return new Weather()
            {
                Time = DateTime.UtcNow,
                Temperature = SensorFusionEstimator.Estimate(bmp280Temp, bmp280Sensor.Precision,
                                                             aht20Temp, aht20Sensor.Precision),
                Pressure = bmp280Sensor.GetPressure(),
                Humidity = aht20Sensor.GetHumidity(),
                Aht20Temperature = aht20Temp,
                Bmp280Temperature= bmp280Temp,
            };
        }
    }
}
