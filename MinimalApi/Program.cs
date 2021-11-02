using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinimalApi.Extensions;
using MinimalApi.Pipelines;
using MinimalApi.Weather.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(WeatherForecastEndpoint));
builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssembly(typeof(Program).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddEndpointDefinitions(builder.Configuration, typeof(WeatherForecastEndpoint));


var app = builder.Build();

app.UseExceptionHandler(builder => builder.UseProblemDetails(app.Environment));


if (!app.Environment.IsDevelopment())
{

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseEndpointDefinitions();

app.Run();