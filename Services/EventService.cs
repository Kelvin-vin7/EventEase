using EventEase.Models;

namespace EventEase.Services;

public class EventService
{
    private readonly List<EventItem> _events = new()
    {
        new EventItem { Id = 1, Name = "Tech Conference 2026", Date = new DateTime(2026, 12, 10), Location = "Jakarta Convention Center", Description = "Konferensi teknologi tahunan: AI, cloud, dan pengembangan web.", Capacity = 100 },
        new EventItem { Id = 2, Name = "Blazor Workshop", Date = new DateTime(2026, 12, 18), Location = "BINUS Kemanggisan, Ruang 301", Description = "Workshop praktik membuat aplikasi web dengan Blazor dan C#.", Capacity = 30 },
        new EventItem { Id = 3, Name = "UI/UX Design Meetup", Date = new DateTime(2027, 1, 8), Location = "Co-working Space Sudirman", Description = "Berbagi pengalaman desain antarmuka dan riset pengguna.", Capacity = 50 },
        new EventItem { Id = 4, Name = "Career Day Informatika", Date = new DateTime(2027, 1, 22), Location = "Aula Utama Kampus", Description = "Bertemu perusahaan dan pelajari peluang magang serta karier.", Capacity = 200 },
    };

    private readonly object _lock = new();

    public List<EventItem> GetAll()
    {
        lock (_lock) return _events.OrderBy(e => e.Date).ToList();
    }

    public EventItem? GetById(int id)
    {
        lock (_lock) return _events.FirstOrDefault(e => e.Id == id);
    }

    public void Update(EventItem updated)
    {
        lock (_lock)
        {
            var existing = _events.FirstOrDefault(e => e.Id == updated.Id);
            if (existing is null) return;
            existing.Name = updated.Name;
            existing.Date = updated.Date;
            existing.Location = updated.Location;
            existing.Description = updated.Description;
            existing.Capacity = updated.Capacity;
        }
    }
}
