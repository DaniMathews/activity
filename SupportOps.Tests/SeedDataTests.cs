using Xunit;

public class SeedDataTests
{
    [Fact]
    public void CreateCannedResponses_ReturnsThreePerTenant()
    {
        // Arrange
        var cannedResponses = SeedData.CreateCannedResponses();

        // Act
        var acmeCount = cannedResponses.Count(r => r.TenantId == "acme");
        var globexCount = cannedResponses.Count(r => r.TenantId == "globex");

        // Assert
        Assert.Equal(3, acmeCount);
        Assert.Equal(3, globexCount);
    }
}
