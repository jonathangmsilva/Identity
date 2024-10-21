using Microsoft.EntityFrameworkCore;
using ProfileApi.Database;

namespace ProfileApi.Extensions
{
  public static class MigrationExtensions
  {
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
      using var scope = app.ApplicationServices.CreateScope();

      var services = scope.ServiceProvider;
      var dbContext = services.GetRequiredService<ApplicationDbContext>();

      dbContext.Database.Migrate();
    }
  }
}
