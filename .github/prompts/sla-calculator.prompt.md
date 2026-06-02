# SLA Calculator Prompt

Add SLA calculation to SupportOps.

## First, Compare Prompt Quality

Start with this weak prompt and observe the output:

```text
Add SLA calculation to tickets.
```

Then use the detailed prompt below.

## Detailed Prompt

Add a service called `SlaCalculator`.

It should expose:

```csharp
DateTimeOffset CalculateDueDate(string tenantPlan, string priority, DateTimeOffset createdAt)
```

Rules:

- Free + Low = 5 days
- Free + Medium = 3 days
- Free + High = 2 days
- Free + Urgent = 1 day
- Pro + Low = 3 days
- Pro + Medium = 2 days
- Pro + High = 1 day
- Pro + Urgent = 8 hours
- Enterprise + Low = 2 days
- Enterprise + Medium = 1 day
- Enterprise + High = 8 hours
- Enterprise + Urgent = 4 hours

Use these examples:

Example 1:

- Plan = Free
- Priority = Low
- CreatedAt = 2026-04-29T09:00:00Z
- Expected due date = 2026-05-04T09:00:00Z

Example 2:

- Plan = Pro
- Priority = Urgent
- CreatedAt = 2026-04-29T09:00:00Z
- Expected due date = 2026-04-29T17:00:00Z

Example 3:

- Plan = Enterprise
- Priority = High
- CreatedAt = 2026-04-29T09:00:00Z
- Expected due date = 2026-04-29T17:00:00Z

## Implementation Requirements

- Keep the code simple and readable.
- Add unit tests for at least three combinations.
- Update `POST /tickets` so new tickets receive `SlaDueAt`.
- Do not put all SLA logic inside the endpoint handler.
- Explain any assumptions.