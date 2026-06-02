public class Ticket
{
    public Guid Id { get; set; }
    public string TenantId { get; set; } = "";
    public string Subject { get; set; } = "";
    public string Description { get; set; } = "";
    public string Status { get; set; } = "Open";      // Open | InProgress | Resolved
    public string Priority { get; set; } = "Medium";  // Low | Medium | High | Urgent
    public string Plan { get; set; } = "Free";        // Free | Pro | Enterprise
    public DateTime CreatedAt { get; set; }
    public DateTime SlaDueAt { get; set; }
}
