using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public static class Extensions
    {
        /*
        pokreće EF Core migracije automatski pri startu aplikacije,
        izbjegava ručno pokretanje dotnet ef database update,
        koristi DI scope da sigurno dobije DbContext,
        omogućava elegantan i čist kod u Program.cs.
        */
        public static /*async*/ IApplicationBuilder UseMigration(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            /*await*/ context.Database.MigrateAsync();

            return app;
        }
    }
}
