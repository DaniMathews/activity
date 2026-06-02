# Agent Feature Prompt

Implement the following SupportOps product issue.

## Issue: Add Canned Responses for Support Agents

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
- Sort canned responses by `Title`.
- Add tests or manual verification steps for tenant isolation.

## Constraints

- Keep storage consistent with the current app.
- Use the `X-Tenant-Id` request header for tenant context unless the app already has a better mechanism.
- Keep endpoint handlers small.
- Do not rewrite unrelated parts of the application.

## Review Expectations

After implementation, summarize:

- Files changed
- Endpoints added
- Tenant isolation behavior
- Tests or manual checks added
- Any assumptions or gaps