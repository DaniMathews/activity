var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<List<Ticket>>(SeedData.CreateTickets());
builder.Services.AddSingleton<List<CannedResponse>>(SeedData.CreateCannedResponses());

var app = builder.Build();

// ---------------------------------------------------------------------------
// Helpers
// ---------------------------------------------------------------------------

bool TryGetTenant(HttpRequest request, out string tenantId)
{
    tenantId = request.Headers["X-Tenant-Id"].FirstOrDefault() ?? "";
    return !string.IsNullOrWhiteSpace(tenantId);
}

string[] validPriorities = ["Low", "Medium", "High", "Urgent"];
string[] validPlans     = SlaCalculator.ValidPlans;

// ---------------------------------------------------------------------------
// Endpoints
// ---------------------------------------------------------------------------

// GET /health  — no tenant required
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

// GET /tickets  — returns only the calling tenant's tickets
app.MapGet("/tickets", (HttpRequest request, List<Ticket> tickets) =>
{
    if (!TryGetTenant(request, out var tenantId))
        return Results.BadRequest("X-Tenant-Id header is required");

    var result = tickets.Where(t => t.TenantId == tenantId).ToList();
    return Results.Ok(result);
});

// GET /canned-responses  — returns only the calling tenant's canned responses sorted by title
app.MapGet("/canned-responses", (HttpRequest request, List<CannedResponse> cannedResponses) =>
{
    if (!TryGetTenant(request, out var tenantId))
        return Results.BadRequest("X-Tenant-Id header is required");

    var result = cannedResponses
        .Where(r => r.TenantId == tenantId)
        .OrderBy(r => r.Title)
        .ToList();

    return Results.Ok(result);
});

// GET /tickets/breaching-sla  — tickets past their SLA due date (excluding Resolved)
app.MapGet("/tickets/breaching-sla", (HttpRequest request, List<Ticket> tickets) =>
{
    if (!TryGetTenant(request, out var tenantId))
        return Results.BadRequest("X-Tenant-Id header is required");

    var now = DateTime.UtcNow;
    var result = tickets
        .Where(t => t.TenantId == tenantId && t.SlaDueAt < now && t.Status != "Resolved")
        .ToList();
    return Results.Ok(result);
});

// GET /tickets/{id}  — 404 if missing or belongs to a different tenant
app.MapGet("/tickets/{id:guid}", (Guid id, HttpRequest request, List<Ticket> tickets) =>
{
    if (!TryGetTenant(request, out var tenantId))
        return Results.BadRequest("X-Tenant-Id header is required");

    var ticket = tickets.FirstOrDefault(t => t.Id == id && t.TenantId == tenantId);
    return ticket is null ? Results.NotFound() : Results.Ok(ticket);
});

// POST /tickets  — create a new ticket for the calling tenant
app.MapPost("/tickets", (HttpRequest request, List<Ticket> tickets, CreateTicketRequest body) =>
{
    if (!TryGetTenant(request, out var tenantId))
        return Results.BadRequest("X-Tenant-Id header is required");

    if (string.IsNullOrWhiteSpace(body.Subject))
        return Results.BadRequest("Subject is required");

    if (!validPriorities.Contains(body.Priority))
        return Results.BadRequest("Priority must be Low, Medium, High, or Urgent");

    var plan = body.Plan ?? "Free";
    if (!validPlans.Contains(plan))
        return Results.BadRequest("Plan must be Free, Pro, or Enterprise");

    var now = DateTime.UtcNow;
    var ticket = new Ticket
    {
        Id          = Guid.NewGuid(),
        TenantId    = tenantId,
        Subject     = body.Subject,
        Description = body.Description ?? "",
        Priority    = body.Priority,
        Plan        = plan,
        Status      = "Open",
        CreatedAt   = now,
        SlaDueAt    = SlaCalculator.GetDueDate(body.Priority, now, plan),
    };

    tickets.Add(ticket);
    return Results.Created($"/tickets/{ticket.Id}", ticket);
});

app.Run();

// ---------------------------------------------------------------------------
// Request DTOs
// ---------------------------------------------------------------------------

record CreateTicketRequest(string Subject, string? Description, string Priority, string? Plan);
