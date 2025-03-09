using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.application.data.services;
using Microsoft.Extensions.DependencyInjection;

namespace bg.hackathon.alphahackers.application.ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IPerfilServices, PerfilServices>();
            services.AddScoped<IBusquedaServices, BusquedaServices>();

            return services;
        }
    }
}
