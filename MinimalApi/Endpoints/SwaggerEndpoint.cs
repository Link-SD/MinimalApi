
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinimalApi.Helpers.Endpoints;
using MinimalApi.Middleware;

namespace MinimalApi.Endpoints;

public class SwaggerEndpoint : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
           // app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalApi v1"));
        }
    }

    public void RegisterServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer(); 
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "MinimalApi", Version = "v1" });
        });
    }
}
