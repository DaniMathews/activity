using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class CannedResponsesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CannedResponsesApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetCannedResponses_ReturnsCurrentTenantOnly_AndSortedByTitle()
    {
        // Arrange
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("X-Tenant-Id", "acme");

        // Act
        var responses = await client.GetFromJsonAsync<List<CannedResponseDto>>("/canned-responses");

        // Assert
        Assert.NotNull(responses);
        Assert.Equal(3, responses.Count);
        Assert.All(responses, r => Assert.Equal("acme", r.TenantId));

        var titles = responses.Select(r => r.Title).ToList();
        var sorted = titles.OrderBy(t => t, StringComparer.Ordinal).ToList();
        Assert.Equal(sorted, titles);
    }

    [Fact]
    public async Task PostCommentFromTemplate_RejectsTemplateFromAnotherTenant()
    {
        // Arrange
        var client = _factory.CreateClient();

        var acmeTicketId = await GetFirstTicketId(client, "acme");
        var globexTemplateId = await GetFirstTemplateId(client, "globex");

        client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        client.DefaultRequestHeaders.Add("X-Tenant-Id", "acme");

        // Act
        var result = await client.PostAsJsonAsync($"/tickets/{acmeTicketId}/comments/from-template", new
        {
            cannedResponseId = globexTemplateId,
        });

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task PostCommentFromTemplate_AcceptsSameTenantTemplate()
    {
        // Arrange
        var client = _factory.CreateClient();

        var acmeTicketId = await GetFirstTicketId(client, "acme");
        var acmeTemplates = await GetTemplates(client, "acme");
        var acmeTemplate = acmeTemplates.First();

        client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        client.DefaultRequestHeaders.Add("X-Tenant-Id", "acme");

        // Act
        var result = await client.PostAsJsonAsync($"/tickets/{acmeTicketId}/comments/from-template", new
        {
            cannedResponseId = acmeTemplate.Id,
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        var comment = await result.Content.ReadFromJsonAsync<TicketCommentDto>();
        Assert.NotNull(comment);
        Assert.Equal(acmeTicketId, comment.TicketId);
        Assert.Equal("acme", comment.TenantId);
        Assert.Equal(acmeTemplate.Body, comment.Body);
    }

    private static async Task<Guid> GetFirstTicketId(HttpClient client, string tenantId)
    {
        client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId);
        var tickets = await client.GetFromJsonAsync<List<TicketDto>>("/tickets");
        Assert.NotNull(tickets);
        return tickets.First().Id;
    }

    private static async Task<Guid> GetFirstTemplateId(HttpClient client, string tenantId)
    {
        var templates = await GetTemplates(client, tenantId);
        return templates.First().Id;
    }

    private static async Task<List<CannedResponseDto>> GetTemplates(HttpClient client, string tenantId)
    {
        client.DefaultRequestHeaders.Remove("X-Tenant-Id");
        client.DefaultRequestHeaders.Add("X-Tenant-Id", tenantId);
        var templates = await client.GetFromJsonAsync<List<CannedResponseDto>>("/canned-responses");
        Assert.NotNull(templates);
        return templates;
    }

    private record TicketDto(Guid Id);
    private record CannedResponseDto(Guid Id, string TenantId, string Title, string Body);
    private record TicketCommentDto(Guid TicketId, string TenantId, string Body);
}
