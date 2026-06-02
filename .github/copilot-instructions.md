# SupportOps Copilot Instructions

SupportOps is a multi-tenant support ticket application built during a GitHub Copilot hackathon.

## Engineering Rules

- Every ticket query must be scoped by `TenantId`.
- Every canned-response query must be scoped by `TenantId`.
- For hackathon simplicity, tenant identity can be read from the `X-Tenant-Id` request header.
- Do not log sensitive values such as email addresses, tokens, IP addresses, ticket descriptions, or authorization headers at Information level.
- Prefer small endpoint handlers that delegate business logic to services when logic grows.
- Validation errors should return clear ProblemDetails-style responses or consistent validation payloads.
- Unit tests should follow Arrange, Act, Assert.
- Keep examples simple enough for a 20-40 minute exercise.
- Do not introduce new packages unless there is a clear reason.
- Do not replace the whole app structure when a small focused change is enough.

## Domain Rules

- Ticket priority values are `Low`, `Medium`, `High`, and `Urgent`.
- Ticket status values are `Open`, `InProgress`, and `Resolved`.
- New tickets default to `Open`.
- `CreatedAt` and `SlaDueAt` should be set by the server.

## SLA Rules

| Plan | Low | Medium | High | Urgent |
|---|---:|---:|---:|---:|
| Free | 5 days | 3 days | 2 days | 1 day |
| Pro | 3 days | 2 days | 1 day | 8 hours |
| Enterprise | 2 days | 1 day | 8 hours | 4 hours |