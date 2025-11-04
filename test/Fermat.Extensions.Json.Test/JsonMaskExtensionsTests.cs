using Fermat.Extensions.Json;

namespace Fermat.Extensions.Json.Test;

public class JsonMaskExtensionsTests
{
    [Fact]
    public void MaskSensitiveData_WithPasswordProperty_MasksPassword()
    {
        // Arrange
        var json = "{\"username\":\"john\",\"password\":\"secret123\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("username", result);
        Assert.Contains("john", result);
        Assert.Contains("password", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("secret123", result);
    }

    [Fact]
    public void MaskSensitiveData_WithTokenProperty_MasksToken()
    {
        // Arrange
        var json = "{\"userId\":123,\"token\":\"abc123xyz\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("userId", result);
        Assert.Contains("token", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("abc123xyz", result);
    }

    [Fact]
    public void MaskSensitiveData_WithCustomMaskPattern_UsesCustomPattern()
    {
        // Arrange
        var json = "{\"password\":\"secret123\"}";
        var customMask = "[REDACTED]";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json, customMask);

        // Assert
        Assert.NotNull(result);
        Assert.Contains(customMask, result);
        Assert.DoesNotContain("secret123", result);
    }

    [Fact]
    public void MaskSensitiveData_WithCustomSensitiveProperties_MasksCustomProperties()
    {
        // Arrange
        var json = "{\"username\":\"john\",\"email\":\"john@example.com\"}";
        var customProperties = new[] { "email" };

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json, "***MASKED***", customProperties);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("username", result);
        Assert.Contains("john", result);
        Assert.Contains("email", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("john@example.com", result);
    }

    [Fact]
    public void MaskSensitiveData_WithNestedObjects_MasksNestedProperties()
    {
        // Arrange
        var json = "{\"user\":{\"name\":\"John\",\"password\":\"secret123\"}}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("user", result);
        Assert.Contains("name", result);
        Assert.Contains("John", result);
        Assert.Contains("password", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("secret123", result);
    }

    [Fact]
    public void MaskSensitiveData_WithArray_MasksArrayItems()
    {
        // Arrange
        var json = "[{\"id\":1,\"password\":\"pass1\"},{\"id\":2,\"password\":\"pass2\"}]";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("id", result);
        Assert.Contains("password", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("pass1", result);
        Assert.DoesNotContain("pass2", result);
    }

    [Fact]
    public void MaskSensitiveData_WithMultipleSensitiveProperties_MasksAll()
    {
        // Arrange
        var json = "{\"username\":\"john\",\"password\":\"pass123\",\"token\":\"token456\",\"apiKey\":\"key789\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("username", result);
        Assert.Contains("john", result);
        Assert.Contains("password", result);
        Assert.Contains("token", result);
        Assert.Contains("apiKey", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("pass123", result);
        Assert.DoesNotContain("token456", result);
        Assert.DoesNotContain("key789", result);
    }

    [Fact]
    public void MaskSensitiveData_WithCaseInsensitiveProperty_MasksProperty()
    {
        // Arrange
        var json = "{\"PASSWORD\":\"secret123\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("PASSWORD", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("secret123", result);
    }

    [Fact]
    public void MaskSensitiveData_WithConnectionString_MasksConnectionStringValues()
    {
        // Arrange
        var json = "{\"connectionString\":\"Password=secret123;Server=localhost\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("connectionString", result);
        Assert.Contains("Password=***MASKED***", result);
        Assert.DoesNotContain("secret123", result);
    }

    [Fact]
    public void MaskSensitiveData_NullInput_ReturnsNull()
    {
        // Arrange
        string? json = null;

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void MaskSensitiveData_EmptyString_ReturnsEmptyString()
    {
        // Arrange
        var json = "";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.Equal("", result);
    }

    [Fact]
    public void MaskSensitiveData_InvalidJson_FallsBackToRegexMasking()
    {
        // Arrange
        var invalidJson = "{\"password\":\"secret123\" invalid}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(invalidJson);

        // Assert
        Assert.NotNull(result);
        // Should still attempt to mask using regex
        Assert.Contains("password", result);
    }

    [Fact]
    public void MaskSensitiveData_WithNonSensitiveProperties_LeavesThemUnchanged()
    {
        // Arrange
        var json = "{\"username\":\"john\",\"age\":30,\"city\":\"New York\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("username", result);
        Assert.Contains("john", result);
        Assert.Contains("age", result);
        Assert.Contains("30", result);
        Assert.Contains("city", result);
        Assert.Contains("New York", result);
    }

    [Fact]
    public void MaskSensitiveData_WithSsnProperty_MasksSsn()
    {
        // Arrange
        var json = "{\"name\":\"John\",\"ssn\":\"123-45-6789\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("name", result);
        Assert.Contains("John", result);
        Assert.Contains("ssn", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("123-45-6789", result);
    }

    [Fact]
    public void MaskSensitiveData_WithCreditCardProperty_MasksCreditCard()
    {
        // Arrange
        var json = "{\"cardholder\":\"John Doe\",\"card\":\"4111111111111111\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("cardholder", result);
        Assert.Contains("John Doe", result);
        Assert.Contains("card", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("4111111111111111", result);
    }

    [Fact]
    public void MaskSensitiveData_WithSecretProperty_MasksSecret()
    {
        // Arrange
        var json = "{\"appName\":\"MyApp\",\"secret\":\"my-secret-key\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("appName", result);
        Assert.Contains("MyApp", result);
        Assert.Contains("secret", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("my-secret-key", result);
    }

    [Fact]
    public void MaskSensitiveData_WithComplexNestedStructure_MasksCorrectly()
    {
        // Arrange
        var json = "{\"user\":{\"profile\":{\"name\":\"John\",\"password\":\"pass123\"},\"token\":\"token123\"},\"apiKey\":\"key123\"}";

        // Act
        var result = JsonMaskExtensions.MaskSensitiveData(json);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("user", result);
        Assert.Contains("profile", result);
        Assert.Contains("name", result);
        Assert.Contains("John", result);
        Assert.Contains("password", result);
        Assert.Contains("token", result);
        Assert.Contains("apiKey", result);
        Assert.Contains("***MASKED***", result);
        Assert.DoesNotContain("pass123", result);
        Assert.DoesNotContain("token123", result);
        Assert.DoesNotContain("key123", result);
    }
}

