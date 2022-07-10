using System.Text.Json;
using WellFlix.Catalog.Api.Extensions;

namespace WellFlix.Catalog.Api.Configuration;

/// <summary>
/// Policy json snake case
/// </summary>
public class JsonSnakeCasePolicy : JsonNamingPolicy
{
    /// <summary>
    /// Json snake case policy instance
    /// </summary>
    public static JsonSnakeCasePolicy Instance { get; } = new();

    /// <summary>
    /// Convert to snake case
    /// </summary>
    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}