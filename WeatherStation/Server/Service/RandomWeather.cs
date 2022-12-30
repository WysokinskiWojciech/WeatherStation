using System;
using WeatherStation.Shared;

namespace WeatherStation.Server.Service
{
    public class RandomWeather : IWeatherService
    {
        public Weather GetCurrentWeather()
        {
            return new Weather()
            {
                Time = System.DateTime.UtcNow,
                Pressure = Math.Round(new Random().NextDouble() * 100 + 900, 2),
                Temperature = Math.Round(new Random().NextDouble() * 30, 2),
            };
        }
    }
}
