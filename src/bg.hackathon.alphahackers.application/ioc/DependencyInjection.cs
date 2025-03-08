using Microsoft.Extensions.DependencyInjection;

namespace bg.hackathon.alphahackers.application.ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
