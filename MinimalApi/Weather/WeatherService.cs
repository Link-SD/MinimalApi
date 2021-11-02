using MinimalApi.Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Weather
{
    public interface IWeatherService
    {
        IEnumerable<WeatherForecast> GetForecasts();
        WeatherForecast GetForecastFor(DayOfWeek day);
    }

    public class WeatherService : IWeatherService
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecast GetForecastFor(DayOfWeek day)
        {
            int today = (int)DateTime.Today.DayOfWeek;
            return new WeatherForecast
            {
                Date = DateTime.Now.AddDays((int)day - today),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };
        }

        public IEnumerable<WeatherForecast> GetForecasts()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            });
        }
    }
}
