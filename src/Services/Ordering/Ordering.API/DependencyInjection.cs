namespace Ordering.API
{
    public static class DependencyInjection // za svaki layer korisitimo DependencyInjection klasu za separation of concerrns (podjelu odgovornosti) i naravno lakseg odrzavanja
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //services.AddCarter()
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            //app.MapCarter();
            return app;
        }
    }
}
