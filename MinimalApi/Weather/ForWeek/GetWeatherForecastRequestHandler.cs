using MediatR;
using Microsoft.Extensions.Logging;
using MinimalApi.Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinimalApi.Weather.ForWeek
{
    public class GetWeatherForecastRequestHandler : IRequestHandler<GetWeatherForecastRequestHandler.Request, IEnumerable<WeatherForecast>?>
    {
        private readonly ILogger<GetWeatherForecastRequestHandler> _logger;
        private readonly IWeatherService _weatherService;

        public GetWeatherForecastRequestHandler(ILogger<GetWeatherForecastRequestHandler> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        public async Task<IEnumerable<WeatherForecast>?> Handle(Request request, CancellationToken cancellationToken)
        {
            throw new Exception("This is an unexpected error...");
            await Task.Delay(1000);

            return _weatherService.GetForecasts(); ;
        }

        public class Request : IRequest<IEnumerable<WeatherForecast>?>
        {
        }
    }
}
