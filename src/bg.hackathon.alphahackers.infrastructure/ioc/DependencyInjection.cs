using bg.hackathon.alphahackers.application.data.interfaces.repositories;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.infrastructure.data.repositories;
using bg.hackathon.alphahackers.infrastructure.data.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bg.hackathon.alphahackers.infrastructure.ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpClient();

            //servicios
            services.AddScoped<IHttpRequestService, HttpRequestService>();

            // repositorios
            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IBusquedaRepository, BusquedaRepository>();


            return services;
        }
    }
}
