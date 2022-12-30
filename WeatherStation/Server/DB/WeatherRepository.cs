using LiteDB;
using WeatherStation.Shared;

namespace WebApplication2.DB
{
    public class WeatherRepository
    {
        private readonly LiteDatabase db;
        private string doc = "Weather";
        public WeatherRepository()
        {
            this.db = new LiteDatabase("Data.db");
        }

        //public List<Weather> GetWeather(DateTime from, DateTime to)
        //{
        //return db.GetCollection<Weather>(doc).;
        //}
    }
}
