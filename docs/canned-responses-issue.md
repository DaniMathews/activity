# Issue: Add Canned Responses for Support Agents

Support agents repeatedly type the same responses. Add canned responses so they can reply faster.

## Requirements

- Add a `CannedResponse` model with:
  - `Id`
  - `TenantId`
  - `Title`
  - `Body`
  - `CreatedAt`
- Seed three sample canned responses per tenant.
- Add `GET /canned-responses` scoped by tenant.
- Add `POST /tickets/{id}/comments/from-template`.
- Ensure users cannot apply a canned response from another tenant.
- Add tests for tenant isolation where practical.

## Mid-Session Steering Comment

After the agent starts, add this comment:

```text
Please also make sure canned responses are sorted by Title and that the endpoint returns only responses for the current tenant.
```

## Acceptance Criteria

- A tenant can list only its own canned responses.
- A tenant cannot apply another tenant's canned response to a ticket.
- Canned responses are sorted by title.
- The implementation follows existing SupportOps conventions.
- The PR or branch includes tests or manual verification notes.