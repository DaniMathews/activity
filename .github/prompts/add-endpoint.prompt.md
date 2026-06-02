# Add SupportOps Endpoint

Create a new API endpoint for SupportOps.

Before changing code:

1. Explain the current relevant files.
2. Propose the endpoint contract.
3. Identify tenant-isolation requirements.
4. Identify validation requirements.
5. Identify tests to add.

When implementing:

- Keep endpoint handlers small.
- Reuse existing services where possible.
- Scope all ticket and canned-response data by `TenantId`.
- Use the `X-Tenant-Id` request header for hackathon tenant context unless the app already has a better mechanism.
- Add tests or manual verification steps for success, validation failure, and tenant isolation.

## Endpoint To Add

Add:

```http
GET /tickets/breaching-sla
```

Rules:

- Return tickets where `SlaDueAt < DateTimeOffset.UtcNow`.
- Exclude tickets with status `Resolved`.
- Scope results by tenant.
- Keep the response shape consistent with existing ticket endpoints.