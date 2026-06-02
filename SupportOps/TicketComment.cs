public class TicketComment
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public string TenantId { get; set; } = "";
    public string Body { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}
