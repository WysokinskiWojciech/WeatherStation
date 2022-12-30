using WeatherStation.Shared;

namespace WeatherStation.Client.ViewModel
{
    public class WeatherVM
    {
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public static WeatherVM Create(Weather weather)
        {
            return new WeatherVM()
            {
                Pressure = weather.Pressure,
                Temperature = weather.Temperature
            };
        }
    }
}
