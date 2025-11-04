using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Fermat.Extensions.Json;

/// <summary>
/// Provides extension methods for formatting JSON strings.
/// </summary>
public static class JsonFormatExtensions
{
    private static readonly JsonSerializerOptions PrettyPrintOptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
    };

    private static readonly JsonSerializerOptions CompactOptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = false
    };

    /// <summary>
    /// Formats a JSON string with indentation for readability.
    /// </summary>
    /// <param name="json">The JSON string to format.</param>
    /// <returns>The formatted JSON string, or null if the input is null or invalid.</returns>
    public static string? PrettyPrint(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return json;
        }

        try
        {
            using var doc = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(doc, PrettyPrintOptions);
        }
        catch
        {
            return json;
        }
    }

    /// <summary>
    /// Formats a JSON string by removing indentation and whitespace.
    /// </summary>
    /// <param name="json">The JSON string to compact.</param>
    /// <returns>The compacted JSON string, or null if the input is null or invalid.</returns>
    public static string? Compact(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return json;
        }

        try
        {
            using var doc = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(doc, CompactOptions);
        }
        catch
        {
            return json;
        }
    }

    /// <summary>
    /// Formats a JSON string with custom indentation.
    /// </summary>
    /// <param name="json">The JSON string to format.</param>
    /// <param name="indent">Whether to use indentation. Default is true.</param>
    /// <returns>The formatted JSON string, or null if the input is null or invalid.</returns>
    public static string? Format(string? json, bool indent = true)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return json;
        }

        try
        {
            using var doc = JsonDocument.Parse(json);
            var options = indent ? PrettyPrintOptions : CompactOptions;
            return JsonSerializer.Serialize(doc, options);
        }
        catch
        {
            return json;
        }
    }
}

