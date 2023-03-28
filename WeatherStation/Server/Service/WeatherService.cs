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
            return new Weather()
            {
                Time = DateTime.UtcNow,
                Temperature = SensorFusionEstimator.Estimate(bmp280Sensor.GetTemperature(), bmp280Sensor.Precision,
                                                             aht20Sensor.GetTemperature(), aht20Sensor.Precision),
                Pressure = bmp280Sensor.GetPressure(),
                Humidity = aht20Sensor.GetHumidity(),
            };
        }
    }
}
