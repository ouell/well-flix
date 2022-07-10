namespace WellFlix.Catalog.Api.Extensions;

/// <summary>
/// String snake case extension
/// </summary>
public static class StringSnakeCaseExtension
{
    
    /// <summary>
    /// Convert string to snake case
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToSnakeCase(this string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }
}