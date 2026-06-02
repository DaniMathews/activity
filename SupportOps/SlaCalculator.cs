public static class SlaCalculator
{
    public static readonly string[] ValidPlans = ["Free", "Pro", "Enterprise"];

    // Key: (plan, priority) → hours until SLA due
    private static readonly Dictionary<(string Plan, string Priority), int> SlaHours = new()
    {
        { ("Free",       "Low"),    120 },  // 5 days
        { ("Free",       "Medium"),  72 },  // 3 days
        { ("Free",       "High"),    48 },  // 2 days
        { ("Free",       "Urgent"),  24 },  // 1 day
        { ("Pro",        "Low"),     72 },  // 3 days
        { ("Pro",        "Medium"),  48 },  // 2 days
        { ("Pro",        "High"),    24 },  // 1 day
        { ("Pro",        "Urgent"),   8 },  // 8 hours
        { ("Enterprise", "Low"),     48 },  // 2 days
        { ("Enterprise", "Medium"),  24 },  // 1 day
        { ("Enterprise", "High"),     8 },  // 8 hours
        { ("Enterprise", "Urgent"),   4 },  // 4 hours
    };

    /// <summary>
    /// Returns the SLA due date for a ticket.
    /// Falls back to Free/Medium (72 h) for any unrecognised plan or priority.
    /// </summary>
    public static DateTime GetDueDate(string priority, DateTime createdAt, string plan = "Free")
    {
        if (!SlaHours.TryGetValue((plan, priority), out var hours))
            hours = 72;

        return createdAt.AddHours(hours);
    }
}
