using Fermat.Extensions.Json;

namespace Fermat.Extensions.Json.Test;

public class JsonFormatExtensionsTests
{
    [Fact]
    public void PrettyPrint_ValidJson_ReturnsFormattedJson()
    {
        // Arrange
        var compactJson = "{\"name\":\"John\",\"age\":30,\"city\":\"New York\"}";

        // Act
        var result = JsonFormatExtensions.PrettyPrint(compactJson);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("\n", result);
        Assert.Contains("  ", result); // Should have indentation
        Assert.Contains("name", result);
        Assert.Contains("John", result);
    }

    [Fact]
    public void PrettyPrint_InvalidJson_ReturnsOriginalString()
    {
        // Arrange
        var invalidJson = "{invalid json}";

        // Act
        var result = JsonFormatExtensions.PrettyPrint(invalidJson);

        // Assert
        Assert.Equal(invalidJson, result);
    }

    [Fact]
    public void PrettyPrint_NullInput_ReturnsNull()
    {
        // Arrange
        string? json = null;

        // Act
        var result = JsonFormatExtensions.PrettyPrint(json);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void PrettyPrint_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        var json = "";

        // Act
        var result = JsonFormatExtensions.PrettyPrint(json);

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void PrettyPrint_WhitespaceString_ReturnsWhitespaceString()
    {
        // Arrange
        var json = "   ";

        // Act
        var result = JsonFormatExtensions.PrettyPrint(json);

        // Assert
        Assert.Equal("   ", result);
    }

    [Fact]
    public void PrettyPrint_ComplexJson_ReturnsFormattedJson()
    {
        // Arrange
        var compactJson = "{\"users\":[{\"id\":1,\"name\":\"John\"},{\"id\":2,\"name\":\"Jane\"}],\"metadata\":{\"count\":2}}";

        // Act
        var result = JsonFormatExtensions.PrettyPrint(compactJson);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("\n", result);
        Assert.Contains("users", result);
        Assert.Contains("John", result);
        Assert.Contains("Jane", result);
    }

    [Fact]
    public void Compact_ValidJson_ReturnsCompactedJson()
    {
        // Arrange
        var prettyJson = @"{
  ""name"": ""John"",
  ""age"": 30,
  ""city"": ""New York""
}";

        // Act
        var result = JsonFormatExtensions.Compact(prettyJson);

        // Assert
        Assert.NotNull(result);
        Assert.DoesNotContain("\n", result);
        Assert.Contains("name", result);
        Assert.Contains("John", result);
    }

    [Fact]
    public void Compact_InvalidJson_ReturnsOriginalString()
    {
        // Arrange
        var invalidJson = "{invalid json}";

        // Act
        var result = JsonFormatExtensions.Compact(invalidJson);

        // Assert
        Assert.Equal(invalidJson, result);
    }

    [Fact]
    public void Compact_NullInput_ReturnsNull()
    {
        // Arrange
        string? json = null;

        // Act
        var result = JsonFormatExtensions.Compact(json);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Compact_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        var json = "";

        // Act
        var result = JsonFormatExtensions.Compact(json);

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void Format_WithIndentTrue_ReturnsFormattedJson()
    {
        // Arrange
        var compactJson = "{\"name\":\"John\",\"age\":30}";

        // Act
        var result = JsonFormatExtensions.Format(compactJson, indent: true);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("\n", result);
        Assert.Contains("  ", result);
    }

    [Fact]
    public void Format_WithIndentFalse_ReturnsCompactedJson()
    {
        // Arrange
        var prettyJson = @"{
  ""name"": ""John"",
  ""age"": 30
}";

        // Act
        var result = JsonFormatExtensions.Format(prettyJson, indent: false);

        // Assert
        Assert.NotNull(result);
        Assert.DoesNotContain("\n", result);
    }

    [Fact]
    public void Format_DefaultIndent_IsTrue()
    {
        // Arrange
        var compactJson = "{\"name\":\"John\",\"age\":30}";

        // Act
        var result = JsonFormatExtensions.Format(compactJson);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("\n", result);
    }

    [Fact]
    public void Format_NullInput_ReturnsNull()
    {
        // Arrange
        string? json = null;

        // Act
        var result = JsonFormatExtensions.Format(json);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Format_InvalidJson_ReturnsOriginalString()
    {
        // Arrange
        var invalidJson = "{invalid}";

        // Act
        var result = JsonFormatExtensions.Format(invalidJson);

        // Assert
        Assert.Equal(invalidJson, result);
    }

    [Fact]
    public void PrettyPrint_WithArray_ReturnsFormattedJson()
    {
        // Arrange
        var compactJson = "[{\"id\":1},{\"id\":2},{\"id\":3}]";

        // Act
        var result = JsonFormatExtensions.PrettyPrint(compactJson);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("\n", result);
        Assert.Contains("id", result);
    }

    [Fact]
    public void Compact_WithNestedObjects_ReturnsCompactedJson()
    {
        // Arrange
        var prettyJson = @"{
  ""user"": {
    ""name"": ""John"",
    ""address"": {
      ""city"": ""NYC""
    }
  }
}";

        // Act
        var result = JsonFormatExtensions.Compact(prettyJson);

        // Assert
        Assert.NotNull(result);
        Assert.DoesNotContain("\n", result);
        Assert.Contains("user", result);
        Assert.Contains("John", result);
        Assert.Contains("NYC", result);
    }
}

