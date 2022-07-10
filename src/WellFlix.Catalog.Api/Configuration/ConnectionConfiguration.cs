using Microsoft.EntityFrameworkCore;
using WellFlix.Catalog.Infra.Data;

namespace WellFlix.Catalog.Api.Configuration;

/// <summary>
/// Connection configuration
/// </summary>
public static class ConnectionConfiguration
{
    
    /// <summary>
    /// Add db connection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddDbConnection(this IServiceCollection services,
                                       IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CatalogDb");
        services.AddDbContext<WellFlixContext>(options => options.UseNpgsql(connectionString));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}