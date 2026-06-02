---
mode: agent
description: Scaffold the initial SupportOps .NET 8 minimal API with in-memory storage, tenant isolation, and SLA calculation.
---

You are scaffolding **SupportOps**, a multi-tenant support-ticket API.
Follow these constraints exactly. Do not add packages, middleware, or layers beyond what is listed.

## Project setup

Create a new .NET 8 minimal API project:

```bash
dotnet new web -n SupportOps
cd SupportOps
```

Delete the generated `app.MapGet("/", ...)` stub. Everything below goes in `Program.cs` unless a separate file is clearly better.

## Tenant identity

Read tenant identity from the `X-Tenant-Id` request header.
If the header is missing or empty, return `400 Bad Request` with the message `"X-Tenant-Id header is required"`.
Do not use an identity provider or authentication middleware.

## Domain model

Add this class (a record is fine):

```csharp
// Ticket.cs
public class Ticket
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = "";
    public string Subject { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "Open";       // Open | InProgress | Resolved
    public string Priority { get; set; } = "Medium";   // Low | Medium | High | Urgent
    public DateTime CreatedAt { get; set; }
    public DateTime SlaDueAt { get; set; }
}
```

Do not add a `CannedResponse` model yet — that is a later activity.

## In-memory store

Use a single `List<Ticket>` registered as a singleton:

```csharp
builder.Services.AddSingleton<List<Ticket>>(SeedData.CreateTickets());
```

## Seed data

Seed six tickets: three for tenant `"acme"` and three for tenant `"globex"`.
Use a mix of priorities (`Low`, `Medium`, `High`, `Urgent`) and statuses.
Set `CreatedAt` to `DateTime.UtcNow` minus a few days so some tickets breach SLA.
Calculate `SlaDueAt` using the SLA helper below.

## SLA calculation

Use the Free plan for all seeded tenants. Calculate `SlaDueAt` by adding the
following hours to `CreatedAt`:

| Priority | Free plan hours |
|----------|----------------|
| Low      | 120 (5 days)   |
| Medium   | 72 (3 days)    |
| High     | 48 (2 days)    |
| Urgent   | 24 (1 day)     |

Add a static helper method `SlaCalculator.GetDueDate(string priority, DateTime createdAt)` that returns the correct `DateTime`.

## Endpoints

Implement exactly these four endpoints for Activity 1:

### GET /health
Returns `200 OK` with `{ "status": "healthy" }`. No tenant header required.

### GET /tickets
- Requires `X-Tenant-Id` header.
- Returns only tickets whose `TenantId` matches the header value.
- Returns `200 OK` with the filtered list (empty array is fine).

### GET /tickets/{id}
- Requires `X-Tenant-Id` header.
- Returns `404 Not Found` if the ticket does not exist **or** belongs to a different tenant.
- Returns `200 OK` with the ticket if found and tenant matches.

### POST /tickets
- Requires `X-Tenant-Id` header.
- Accepts a JSON body with `Subject`, `Description`, and `Priority`.
- Validation rules:
  - `Subject` must not be null or empty → `400` with `"Subject is required"`.
  - `Priority` must be one of `Low`, `Medium`, `High`, `Urgent` (case-sensitive) → `400` with `"Priority must be Low, Medium, High, or Urgent"`.
- On success:
  - Set `Id` to a new `Guid`.
  - Set `TenantId` from the header.
  - Set `Status` to `"Open"`.
  - Set `CreatedAt` to `DateTime.UtcNow`.
  - Set `SlaDueAt` using `SlaCalculator.GetDueDate`.
  - Add to the in-memory list.
  - Return `201 Created` with the created ticket.

## What NOT to build in Activity 1

Do not implement any of the following — they are added in later activities:

- `GET /tickets/breaching-sla`
- `GET /canned-responses`
- `POST /tickets/{id}/comments/from-template`
- SQLite or any database
- Authentication or JWT
- Swagger / OpenAPI UI
- CORS
- Logging configuration

## Verify

After scaffolding, confirm the project builds and the health endpoint responds:

```bash
dotnet build
dotnet run &
curl http://localhost:5000/health
curl -H "X-Tenant-Id: acme" http://localhost:5000/tickets
```