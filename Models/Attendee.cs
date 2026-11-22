namespace EventEase.Models;

public class Attendee
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string? Phone { get; set; }
    public int Tickets { get; set; }
    public bool Attended { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.Now;
}
