using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherStation.Server.Service
{
    public class PeriodicReadingService : BackgroundService
    {
        private readonly IWeatherService weatherService;
        private readonly PeriodicTimer timer = new(TimeSpan.FromMinutes(1));

        public PeriodicReadingService(IWeatherService weatherService)
        {
            this.weatherService = weatherService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (await timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
            {
                var weather = weatherService.GetCurrentWeather();
                //Save to db // to implement
            }
        }
    }
}
