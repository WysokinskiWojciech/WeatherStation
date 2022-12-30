using WeatherStation.Shared;

namespace WeatherStation.Server.Service
{
    public interface IWeatherService
    {
        public Weather GetCurrentWeather();
    }
}