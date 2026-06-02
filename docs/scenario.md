# SupportOps Scenario

SupportOps is a lightweight internal support-ticket application for organizations that need to track issues, comments, priorities, and service-level agreements across multiple tenants.

In this hackathon, attendees build SupportOps from scratch with GitHub Copilot. The goal is not to create a production-ready helpdesk. The goal is to practice how Copilot supports the full software delivery lifecycle:

- Understanding a product idea
- Scaffolding an application
- Adding features
- Writing tests
- Improving prompts
- Creating reusable instructions
- Delegating work to agents
- Reviewing and securing generated code

## Product Story

The first customer wants a small support platform that lets agents:

- View tickets for their tenant
- Create new tickets
- Track priority and SLA due dates
- Find tickets that are breaching SLA
- Add common canned responses
- Ensure one tenant cannot access another tenant's tickets

## Core Concepts

| Concept | Description |
|---|---|
| Tenant | A customer organization using SupportOps |
| User | A person who creates, views, or works on tickets |
| Ticket | A support request with subject, description, priority, status, and SLA due date |
| Comment | A message added to a ticket |
| Canned response | A reusable response template scoped to a tenant |
| SLA | A due date calculated from the tenant plan and ticket priority |

## Target Capabilities

By the end of the activity sequence, each team should have a small app that can:

- Return health status
- List seeded tickets
- Return a ticket by ID
- Create a ticket
- Calculate SLA due dates
- List tickets breaching SLA
- Add canned responses or comments
- Pass basic tests
- Demonstrate at least two security fixes

## Team Mission

Build the first working version of SupportOps using GitHub Copilot as a pair programmer, planner, reviewer, and agent.

Each activity should leave the app in a better state than it started.