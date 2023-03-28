using System;

namespace WeatherStation.Shared
{
    public class Weather
    {
        public Guid Id { get; set; }    
        public DateTime Time { get; set; }  
        public double Temperature { get; set; }
        public double Pressure { get; set; }   
        public double Humidity { get; set; }
    }
}