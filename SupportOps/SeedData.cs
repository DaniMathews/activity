public static class SeedData
{
    public static List<CannedResponse> CreateCannedResponses()
    {
        var now = DateTime.UtcNow;

        return
        [
            new CannedResponse
            {
                Id        = Guid.NewGuid(),
                TenantId  = "acme",
                Title     = "Escalation in Progress",
                Body      = "Thanks for your patience — we have escalated this issue to our engineering team and will follow up shortly.",
                CreatedAt = now.AddDays(-2),
            },
            new CannedResponse
            {
                Id        = Guid.NewGuid(),
                TenantId  = "acme",
                Title     = "Need More Details",
                Body      = "Could you share the exact steps to reproduce this issue and include any error messages you see?",
                CreatedAt = now.AddDays(-3),
            },
            new CannedResponse
            {
                Id        = Guid.NewGuid(),
                TenantId  = "acme",
                Title     = "Issue Resolved",
                Body      = "We have applied a fix and confirmed the issue is resolved. Please let us know if you still see any problems.",
                CreatedAt = now.AddDays(-1),
            },
            new CannedResponse
            {
                Id        = Guid.NewGuid(),
                TenantId  = "globex",
                Title     = "Escalation in Progress",
                Body      = "Thanks for your patience — we have escalated this issue to our engineering team and will follow up shortly.",
                CreatedAt = now.AddDays(-2),
            },
            new CannedResponse
            {
                Id        = Guid.NewGuid(),
                TenantId  = "globex",
                Title     = "Need More Details",
                Body      = "Could you share the exact steps to reproduce this issue and include any error messages you see?",
                CreatedAt = now.AddDays(-3),
            },
            new CannedResponse
            {
                Id        = Guid.NewGuid(),
                TenantId  = "globex",
                Title     = "Issue Resolved",
                Body      = "We have applied a fix and confirmed the issue is resolved. Please let us know if you still see any problems.",
                CreatedAt = now.AddDays(-1),
            },
        ];
    }

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
