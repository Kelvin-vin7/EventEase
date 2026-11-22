using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public class EventItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nama event wajib diisi.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "Nama event 3-80 karakter.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Tanggal wajib diisi.")]
    public DateTime Date { get; set; } = DateTime.Today.AddDays(7);

    [Required(ErrorMessage = "Lokasi wajib diisi.")]
    [StringLength(100, ErrorMessage = "Lokasi maksimal 100 karakter.")]
    public string Location { get; set; } = "";

    [StringLength(300, ErrorMessage = "Deskripsi maksimal 300 karakter.")]
    public string Description { get; set; } = "";

    [Range(1, 1000, ErrorMessage = "Kapasitas harus 1-1000.")]
    public int Capacity { get; set; } = 50;

    public EventItem Clone() => (EventItem)MemberwiseClone();
}
