using BrazilCities.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BrazilCities.Api.Configurations;

internal static class MigrationConfig
{
    private const int MaxAttempts = 5;

    internal static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseMigration");

        for (var attempt = 1; ; attempt++)
        {
            try
            {
                await using var scope = app.Services.CreateAsyncScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var pendingMigrations = (await context.Database.GetPendingMigrationsAsync()).ToList();

                if (pendingMigrations.Count == 0)
                {
                    logger.LogInformation("Database is up to date, no pending migrations");
                    return;
                }

                logger.LogInformation("Applying {Count} pending migration(s): {Migrations}",
                    pendingMigrations.Count, string.Join(", ", pendingMigrations));

                await context.Database.MigrateAsync();

                logger.LogInformation("Database migrations applied successfully");
                return;
            }
            catch (Exception ex) when (ex is NpgsqlException or TimeoutException && attempt < MaxAttempts)
            {
                var delay = TimeSpan.FromSeconds(2 * attempt);
                logger.LogWarning(ex,
                    "Database not available (attempt {Attempt}/{MaxAttempts}), retrying in {Delay}s",
                    attempt, MaxAttempts, delay.TotalSeconds);
                await Task.Delay(delay);
            }
        }
    }
}
