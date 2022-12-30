using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WeatherStation.Server.DB;
using WeatherStation.Server.Service;
using WeatherStation.Shared;

namespace WeatherStation.Server.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService weatherService;
        private readonly WeatherRepository weatherRepository;

        public WeatherController(IWeatherService weatherService,WeatherRepository weatherRepository)
        {
            this.weatherService=weatherService;
            this.weatherRepository = weatherRepository;
        }

        [HttpGet("Current")]
        public Weather Get()
        {
            return weatherService.GetCurrentWeather();
        }

        [HttpGet("History/{from}/{to}")]
        public List<Weather> GetWeatherFromTo(DateTime from, DateTime to)
        {
            return weatherRepository.GetWeather(from,to);
        }

    }
}