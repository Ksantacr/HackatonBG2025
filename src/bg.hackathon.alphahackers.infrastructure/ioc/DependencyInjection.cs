using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.infrastructure.data.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bg.hackathon.alphahackers.infrastructure.ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddScoped<IHttpRequestService, HttpRequestService>();


            return services;
        }
    }
}
