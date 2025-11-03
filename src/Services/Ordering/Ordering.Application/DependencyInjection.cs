using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add MediatR, AutoMapper, FluentValidation, etc. here
            return services;
        }
    }
}
