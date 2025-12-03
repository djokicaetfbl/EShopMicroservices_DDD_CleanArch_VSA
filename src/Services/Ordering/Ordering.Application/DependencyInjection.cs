using BuildingBlocks.Behaviours;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add MediatR, AutoMapper, FluentValidation, etc. here
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            });

            services.AddFeatureManagement(); // Dodaje podršku za Feature Management u aplikaciju.
            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly()); //Vrati mi Assembly (skup svih klasa, interface-a, atributa…) iz trenutnog projekta gdje se ovaj kod izvršava.
            // Drugim riječima
            // Ako pozoveš ovu metodu iz projekta WebAPI, vratiće assembly tog WebAPI-ja.
            // Ako pozoveš iz nekog drugog bibliotečkog projekta, vratiće assembly tog projekta.
            // MassTransit automatski skenira assembly i pronađe sve klase koje nasljeđuju IConsumer<T>.
            // Ako proslijediš Assembly.GetExecutingAssembly(), ti kažeš:
            // "Register all consumers from THIS project’s assembly."
            
            
            return services;
        }
    }
}
