namespace EventEase.Services;

/// <summary>State per sesi pengguna (scoped per koneksi Blazor Server).</summary>
public class UserSessionService
{
    private readonly List<int> _registeredEventIds = new();

    public string? FullName { get; private set; }
    public string? Email { get; private set; }
    public IReadOnlyList<int> RegisteredEventIds => _registeredEventIds;
    public bool HasProfile => !string.IsNullOrWhiteSpace(FullName);

    public event Action? Changed;

    public void SaveProfile(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
        Changed?.Invoke();
    }

    public void AddRegistration(int eventId)
    {
        if (!_registeredEventIds.Contains(eventId)) _registeredEventIds.Add(eventId);
        Changed?.Invoke();
    }

    public bool IsRegistered(int eventId) => _registeredEventIds.Contains(eventId);

    public void Clear()
    {
        FullName = null;
        Email = null;
        _registeredEventIds.Clear();
        Changed?.Invoke();
    }
}
