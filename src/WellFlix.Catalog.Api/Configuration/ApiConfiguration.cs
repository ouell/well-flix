namespace WellFlix.Catalog.Api.Configuration;

/// <summary>
/// Api configuration
/// </summary>
public static class ApiConfiguration
{
    /// <summary>
    /// Add controller configuration
    /// </summary>
    /// <param name="services"></param>
    public static void AddConfigControllers(this IServiceCollection services)
    {
        services.AddControllers()
                .AddJsonOptions(
                     config => config.JsonSerializerOptions.PropertyNamingPolicy = JsonSnakeCasePolicy.Instance);

        services.AddDocumentation();
    }

    /// <summary>
    /// Add swagger documentation
    /// </summary>
    /// <param name="services"></param>
    private static void AddDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    /// <summary>
    /// Application set to use swagger
    /// </summary>
    /// <param name="app"></param>
    public static void UseDocumentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}