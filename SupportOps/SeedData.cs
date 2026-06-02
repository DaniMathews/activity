public static class SeedData
{
    public static List<Ticket> CreateTickets()
    {
        var now = DateTime.UtcNow;

        return
        [
            // acme tickets — Pro plan
            new Ticket
            {
                Id          = Guid.NewGuid(),
                TenantId    = "acme",
                Plan        = "Pro",
                Subject     = "Login page throws 500 error",
                Description = "Users cannot log in since this morning's deploy.",
                Status      = "Open",
                Priority    = "Urgent",
                CreatedAt   = now.AddDays(-2),
                SlaDueAt    = SlaCalculator.GetDueDate("Urgent", now.AddDays(-2), "Pro"),
            },
            new Ticket
            {
                Id          = Guid.NewGuid(),
                TenantId    = "acme",
                Plan        = "Pro",
                Subject     = "Export to CSV is missing columns",
                Description = "The exported file is missing the 'Region' and 'Owner' columns.",
                Status      = "InProgress",
                Priority    = "High",
                CreatedAt   = now.AddDays(-3),
                SlaDueAt    = SlaCalculator.GetDueDate("High", now.AddDays(-3), "Pro"),
            },
            new Ticket
            {
                Id          = Guid.NewGuid(),
                TenantId    = "acme",
                Plan        = "Free",
                Subject     = "Update billing email address",
                Description = "Please update the billing contact to billing@acme.example.",
                Status      = "Open",
                Priority    = "Low",
                CreatedAt   = now.AddDays(-1),
                SlaDueAt    = SlaCalculator.GetDueDate("Low", now.AddDays(-1), "Free"),
            },

            // globex tickets — Enterprise plan
            new Ticket
            {
                Id          = Guid.NewGuid(),
                TenantId    = "globex",
                Plan        = "Enterprise",
                Subject     = "Dashboard takes 30 seconds to load",
                Description = "Performance degraded after the v2.4 release.",
                Status      = "Open",
                Priority    = "High",
                CreatedAt   = now.AddDays(-4),
                SlaDueAt    = SlaCalculator.GetDueDate("High", now.AddDays(-4), "Enterprise"),
            },
            new Ticket
            {
                Id          = Guid.NewGuid(),
                TenantId    = "globex",
                Plan        = "Enterprise",
                Subject     = "Password reset email not arriving",
                Description = "Multiple users report they never receive the reset link.",
                Status      = "Open",
                Priority    = "Urgent",
                CreatedAt   = now.AddDays(-2),
                SlaDueAt    = SlaCalculator.GetDueDate("Urgent", now.AddDays(-2), "Enterprise"),
            },
            new Ticket
            {
                Id          = Guid.NewGuid(),
                TenantId    = "globex",
                Plan        = "Enterprise",
                Subject     = "Add dark mode to the UI",
                Description = "Feature request from the Globex design team.",
                Status      = "Open",
                Priority    = "Medium",
                CreatedAt   = now.AddDays(-1),
                SlaDueAt    = SlaCalculator.GetDueDate("Medium", now.AddDays(-1), "Enterprise"),
            },
        ];
    }
}
