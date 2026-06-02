public class CannedResponse
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = "";
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
