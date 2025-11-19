using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API
{
    public static class DependencyInjection // za svaki layer korisitimo DependencyInjection klasu za separation of concerrns (podjelu odgovornosti) i naravno lakseg odrzavanja
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!); // da pvojerimo status SQL servera
            
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            app.UseHealthChecks("/health", 
            new HealthCheckOptions    
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse // da healthcheck bude lijepo ispisan
            });

            return app;
        }
    }
}
