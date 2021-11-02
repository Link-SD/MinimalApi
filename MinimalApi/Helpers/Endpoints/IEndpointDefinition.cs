using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MinimalApi.Helpers.Endpoints
{
    public interface IEndpointDefinition
    {
        void RegisterServices(IServiceCollection services, IConfiguration configuration);
        void RegisterEndpoints(WebApplication app);
    }
}
