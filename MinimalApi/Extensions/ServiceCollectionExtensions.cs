using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalApi.Helpers.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEndpointDefinitions(this IServiceCollection services, IConfiguration configuration, params Type[] assemblyMarkers)
        {
            var endpointDefinitions = new List<IEndpointDefinition>();
            
            foreach (var marker in assemblyMarkers)
            {
                endpointDefinitions
                    .AddRange(marker.Assembly.ExportedTypes
                        .Where(x => typeof(IEndpointDefinition)
                        .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                        .Select(Activator.CreateInstance)
                        .Cast<IEndpointDefinition>());
            }

            foreach (var definition in endpointDefinitions)
            {
                definition.RegisterServices(services, configuration);
            }

            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);

            return services;
        }

        public static WebApplication UseEndpointDefinitions(this WebApplication app)
        {
            var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

            foreach (var definition in definitions)
            {
                definition.RegisterEndpoints(app);
            }

            return app;
        }
    }
}
