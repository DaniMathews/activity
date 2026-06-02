# Facilitator Notes

These notes help keep the SupportOps build-from-scratch option engaging and predictable.

## Recommended Framing

Do not present this as a blank coding lab. Present it as a product mission:

> You are the first engineering team for SupportOps. Your goal is to use GitHub Copilot to build, improve, delegate, and secure the first version of the product.

## Recommended Team Roles

For teams of 3-5 attendees, assign rotating roles:

| Role | Responsibility |
|---|---|
| Driver | Uses VS Code and Copilot |
| Reviewer | Challenges generated code and asks follow-up prompts |
| Tester | Runs the API and verifies behavior |
| Security lead | Checks tenant isolation and validation |
| Scribe | Captures useful prompts and lessons learned |

Rotate roles after each activity.

## Common Failure Modes

| Failure mode | Facilitator action |
|---|---|
| Copilot creates too much architecture | Remind teams to keep the app minimal and follow `architecture-constraints.md` |
| Teams choose different stacks | Re-anchor on .NET 8 minimal API unless the session intentionally supports multiple stacks |
| No tests are added | Ask for at least manual `curl` verification if time is short |
| Tenant filtering is forgotten | Use this as a teaching moment for Activity 4 and Activity 6 |
| Agent work runs too long | Switch to local Agent mode or have attendees review the proposed plan instead |
| Build breaks near the end | Use the failure as a Copilot debugging exercise, but time-box it |

## Timing Guidance

For mixed-skill audiences, use 35-40 minutes for Activities 2, 3, 5, and 6.

For experienced developers, keep activities closer to 20-25 minutes and spend more time on comparison, demos, and discussion.

## Engagement Techniques

- Run Activity 3 as a prompt battle.
- Award points for tests and secure fixes, not just generated code.
- Ask teams to share one Copilot mistake at the end of each activity.
- Use a 3-minute closing demo per team.
- Encourage teams to capture their best prompt in the chat or shared notes.

## Suggested Scoring

Use `docs/team-scorecard.md` if you want a light competition.

Reward:

- Working behavior
- Passing tests or clear manual verification
- Good prompt quality
- Security awareness
- Human review of Copilot output
- Clear demo explanation

## Activity 5 Fallback

If Copilot Cloud Agent is not available:

1. Use local Agent mode.
2. Paste `.github/prompts/agent-feature.prompt.md` into Chat.
3. Ask Copilot to first produce a plan.
4. Let it implement in small steps.
5. Have the human reviewer challenge tenant isolation before accepting changes.

## Activity 6 Minimum Bar

If time is tight, require only two fixes:

- Tenant isolation on `GET /tickets/{id}`.
- Validation for invalid priority or missing subject.

If time allows, add:

- Cross-tenant canned response protection.
- Logging review.
- Search endpoint review.
- Error response cleanup.