using MediatR;
using Microsoft.Extensions.Logging;
using MinimalApi.Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinimalApi.Weather.ByDay
{
    public class GetWeatherByWeekDayRequestHandler : IRequestHandler<GetWeatherByWeekDayRequestHandler.Request, WeatherForecast?>
    {
        private readonly ILogger<GetWeatherByWeekDayRequestHandler> _logger;
        private readonly IWeatherService _weatherService;

        public GetWeatherByWeekDayRequestHandler(ILogger<GetWeatherByWeekDayRequestHandler> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public async Task<WeatherForecast?> Handle(Request request, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);

            if (!Enum.TryParse<DayOfWeek>(request.Day, true, out var day))
            {
                day = DateTime.Today.DayOfWeek;
            }


            var forecast = _weatherService.GetForecastFor(day);

            return forecast;
        }



        public class Request : IRequest<WeatherForecast?>
        {
            public string? Day { get; set; }
        }
    }
}
