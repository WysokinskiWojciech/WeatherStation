using Microsoft.AspNetCore.Mvc;
using WeatherStation.Server.Service;
using WeatherStation.Shared;

namespace WeatherStation.Server.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            this.weatherService=weatherService;
        }

        [HttpGet("Current")]
        public Weather Get()
        {
            return weatherService.GetCurrentWeather();
        }
    }
}