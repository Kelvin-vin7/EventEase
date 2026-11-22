using EventEase.Models;

namespace EventEase.Services;

public class RegistrationService
{
    private readonly List<Attendee> _attendees = new();
    private readonly EventService _events;
    private readonly object _lock = new();
    private int _nextId = 1;

    public RegistrationService(EventService events) => _events = events;

    public int TotalTickets(int eventId)
    {
        lock (_lock) return _attendees.Where(a => a.EventId == eventId).Sum(a => a.Tickets);
    }

    public int Remaining(EventItem ev) => Math.Max(0, ev.Capacity - TotalTickets(ev.Id));

    public List<Attendee> GetByEvent(int eventId)
    {
        lock (_lock)
            return _attendees.Where(a => a.EventId == eventId).OrderBy(a => a.RegisteredAt).ToList();
    }

    /// <summary>Mendaftarkan peserta. Mengembalikan (attendee, null) bila sukses atau (null, pesanError).</summary>
    public (Attendee? Attendee, string? Error) Register(int eventId, RegistrationModel model)
    {
        var ev = _events.GetById(eventId);
        if (ev is null) return (null, "Event tidak ditemukan.");

        lock (_lock)
        {
            var email = model.Email.Trim();

            if (_attendees.Any(a => a.EventId == eventId &&
                                    a.Email.Equals(email, StringComparison.OrdinalIgnoreCase)))
                return (null, "Email ini sudah terdaftar pada event tersebut.");

            var used = _attendees.Where(a => a.EventId == eventId).Sum(a => a.Tickets);
            var remaining = ev.Capacity - used;
            if (remaining <= 0) return (null, "Maaf, kursi sudah penuh.");
            if (model.Tickets > remaining) return (null, $"Sisa kursi hanya {remaining}.");

            var attendee = new Attendee
            {
                Id = _nextId++,
                EventId = eventId,
                FullName = model.FullName.Trim(),
                Email = email,
                Phone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim(),
                Tickets = model.Tickets,
            };
            _attendees.Add(attendee);
            return (attendee, null);
        }
    }

    public void SetAttended(int attendeeId, bool attended)
    {
        lock (_lock)
        {
            var a = _attendees.FirstOrDefault(x => x.Id == attendeeId);
            if (a is not null) a.Attended = attended;
        }
    }
}
