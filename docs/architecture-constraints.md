# SupportOps Architecture Constraints

These constraints keep the build-from-scratch activity focused and manageable inside 20-40 minute exercise blocks.

## Recommended Stack

- .NET 8 minimal API
- In-memory collection for Activities 1-4
- xUnit or built-in HTTP file/manual `curl` checks for tests
- Optional SQLite only if the group is advanced
- Optional Razor, Blazor, or React UI only after API milestones are complete

## Keep It Small

Do not introduce these unless the facilitator explicitly chooses an advanced lane:

- Full identity provider integration
- Complex database migrations
- Message queues
- Microservices
- Event sourcing
- Kubernetes
- Production-grade UI
- Complex domain-driven design layering

## Required API Shape

The main build should stay close to these endpoints:

```http
GET  /health
GET  /tickets
GET  /tickets/{id}
POST /tickets
GET  /tickets/breaching-sla
GET  /canned-responses
POST /tickets/{id}/comments/from-template
```

## Required Domain Fields

`Ticket` should include at least:

- `Id`
- `TenantId`
- `Subject`
- `Description`
- `Status`
- `Priority`
- `CreatedAt`
- `SlaDueAt`

`CannedResponse` should include at least:

- `Id`
- `TenantId`
- `Title`
- `Body`
- `CreatedAt`

## Tenant Isolation Rule

Every ticket and canned-response query must be scoped by `TenantId`.

For the hackathon, tenant identity can come from a simple request header:

```http
X-Tenant-Id: acme
```

This is intentionally simple. The security discussion should mention that production systems need proper authenticated tenant claims.

## Testing Guidance

Minimum useful test cases:

- `POST /tickets` accepts a valid ticket
- `POST /tickets` rejects missing subject
- `POST /tickets` rejects invalid priority
- SLA calculator returns expected due dates
- Tenant A cannot read Tenant B's ticket
- Tenant A cannot use Tenant B's canned response

## Design Principle

Prefer clarity over architecture. Attendees should spend their time learning Copilot workflows, not debugging unnecessary framework complexity.