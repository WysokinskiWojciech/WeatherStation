using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStation.Shared;

namespace WeatherStation.Server.DB
{
    public class WeatherRepository
    {
        private readonly LiteDatabase db;
        private string doc = "Weather";
        public WeatherRepository()
        {
            this.db = new LiteDatabase("DB//Data.db");
        }

        public List<Weather> GetWeather(DateTime from, DateTime to)
        {
            return db.GetCollection<Weather>(doc).Find(Query.Between(nameof(Weather.Time), from, to)).ToList();
        }

        public void SaveWeather(Weather weather)
        {
            var col = db.GetCollection<Weather>(doc);
            col.Insert(weather);
            col.EnsureIndex(x => x.Id);
        }
    }
}
