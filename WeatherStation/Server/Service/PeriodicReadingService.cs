using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using WeatherStation.Server.DB;

namespace WeatherStation.Server.Service
{
    public class PeriodicReadingService : BackgroundService
    {
        private readonly WeatherRepository weatherRepositiory;
        private readonly IWeatherService weatherService;
        private readonly IConfiguration configuration;
        private readonly PeriodicTimer timer;

        public PeriodicReadingService(IWeatherService weatherService,
                                        WeatherRepository weatherRepository,
                                        IConfiguration configuration)
        {
            this.weatherService = weatherService;
            this.weatherRepositiory = weatherRepository;
            this.configuration = configuration;
            this.timer = new(TimeSpan.FromMinutes(Convert.ToInt32(configuration["ReadPeriodInMinutes"])));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                weatherRepositiory.SaveWeather(weatherService.GetCurrentWeather());
            }
        }
    }
}
