using Xunit;

public class SlaCalculatorTests
{
    // -----------------------------------------------------------------------
    // Free plan
    // -----------------------------------------------------------------------

    [Fact]
    public void FreePlan_LowPriority_Returns5Days()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act
        var due = SlaCalculator.GetDueDate("Low", createdAt, "Free");

        // Assert
        Assert.Equal(createdAt.AddHours(120), due);   // 5 days
    }

    [Fact]
    public void FreePlan_UrgentPriority_Returns1Day()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act
        var due = SlaCalculator.GetDueDate("Urgent", createdAt, "Free");

        // Assert
        Assert.Equal(createdAt.AddHours(24), due);    // 1 day
    }

    // -----------------------------------------------------------------------
    // Pro plan
    // -----------------------------------------------------------------------

    [Fact]
    public void ProPlan_UrgentPriority_Returns8Hours()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act
        var due = SlaCalculator.GetDueDate("Urgent", createdAt, "Pro");

        // Assert
        Assert.Equal(createdAt.AddHours(8), due);
    }

    [Fact]
    public void ProPlan_LowPriority_Returns3Days()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act
        var due = SlaCalculator.GetDueDate("Low", createdAt, "Pro");

        // Assert
        Assert.Equal(createdAt.AddHours(72), due);    // 3 days
    }

    // -----------------------------------------------------------------------
    // Enterprise plan
    // -----------------------------------------------------------------------

    [Fact]
    public void EnterprisePlan_HighPriority_Returns8Hours()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act
        var due = SlaCalculator.GetDueDate("High", createdAt, "Enterprise");

        // Assert
        Assert.Equal(createdAt.AddHours(8), due);
    }

    [Fact]
    public void EnterprisePlan_UrgentPriority_Returns4Hours()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act
        var due = SlaCalculator.GetDueDate("Urgent", createdAt, "Enterprise");

        // Assert
        Assert.Equal(createdAt.AddHours(4), due);
    }

    // -----------------------------------------------------------------------
    // Fallback behaviour
    // -----------------------------------------------------------------------

    [Fact]
    public void UnknownPlan_FallsBackTo72Hours()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act
        var due = SlaCalculator.GetDueDate("High", createdAt, "Gold");

        // Assert
        Assert.Equal(createdAt.AddHours(72), due);    // Free/Medium default
    }

    [Fact]
    public void DefaultPlanParameter_UsesFree()
    {
        // Arrange
        var createdAt = new DateTime(2026, 4, 29, 9, 0, 0, DateTimeKind.Utc);

        // Act — omit the plan parameter entirely
        var due = SlaCalculator.GetDueDate("Low", createdAt);

        // Assert — same as Free + Low = 120 h
        Assert.Equal(createdAt.AddHours(120), due);
    }
}
