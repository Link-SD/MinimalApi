using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Helpers.Endpoints;
using MinimalApi.Weather;
using MinimalApi.Weather.ByDay;
using MinimalApi.Weather.ForWeek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Weather.Endpoints
{
    public class WeatherForecastEndpoint : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            app.MapGet("/api/weather", async (IMediator mediator) => await mediator.Send(new GetWeatherForecastRequestHandler.Request()));
            app.MapGet("/api/weather/{day}", async (HttpContext context, IMediator mediator, string day) => await mediator.Send(new GetWeatherByWeekDayRequestHandler.Request { Day = day }));
        }

        public void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IWeatherService, WeatherService>();
        }
    }
}
