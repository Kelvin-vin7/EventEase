using System.ComponentModel.DataAnnotations;

namespace EventEase.Models;

public class RegistrationModel
{
    [Required(ErrorMessage = "Nama lengkap wajib diisi.")]
    [StringLength(60, MinimumLength = 2, ErrorMessage = "Nama 2-60 karakter.")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Email wajib diisi.")]
    [EmailAddress(ErrorMessage = "Format email tidak valid.")]
    public string Email { get; set; } = "";

    [Phone(ErrorMessage = "Format nomor telepon tidak valid.")]
    public string? Phone { get; set; }

    [Range(1, 5, ErrorMessage = "Jumlah tiket harus 1-5.")]
    public int Tickets { get; set; } = 1;

    [Range(typeof(bool), "true", "true", ErrorMessage = "Anda harus menyetujui syarat & ketentuan.")]
    public bool Agree { get; set; }
}
