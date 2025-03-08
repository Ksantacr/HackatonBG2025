using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.infrastructure.data.context;
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
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

            services.AddHttpClient();
            services.AddScoped<IHttpRequestService, HttpRequestService>();


            return services;
        }
    }
}
