# SupportOps Security Review Prompt

Review the selected SupportOps code for security issues.

Focus on:

- Broken access control
- Missing tenant isolation
- Injection risks
- Sensitive data exposure
- Missing validation
- Weak authentication or authorization
- Error responses that expose implementation details

Return findings in this format:

| Severity | Finding | Risk | File/function | Suggested fix | Test to prove the fix |
|---|---|---|---|---|---|

## GAR Loop

For each selected issue:

1. Generate a fix.
2. Analyze the fix for gaps.
3. Repair the fix and add tests.

## Minimum Issues To Check

- Can Tenant A read Tenant B's ticket?
- Can Tenant A use Tenant B's canned response?
- Are invalid priorities rejected?
- Are ticket descriptions or sensitive request values logged?
- Are ticket queries scoped by tenant?
- Are validation failures returned consistently?

## Output Expectations

After applying fixes, summarize:

- Issues fixed
- Tests added
- Residual risks
- What still needs human review