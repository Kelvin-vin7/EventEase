# EventEase

Aplikasi manajemen event berbasis **Blazor Server (.NET 10)**: daftar event, pendaftaran peserta dengan validasi, state sesi pengguna, dan pelacak kehadiran (Attendance Tracker). Proyek ini menggabungkan hasil tiga aktivitas sebelumnya yang dikerjakan dengan bantuan **Microsoft Copilot**.

## Fitur

| Fitur | Lokasi |
|---|---|
| Event Card dengan field dan *two-way data binding* | `Components/EventCard.razor` |
| Routing (`/`, `/events`, `/events/{id}`, `/register/{id}`, `/attendance`, halaman *not found*) | `Components/Pages/*`, `Components/Routes.razor` |
| Registration Form dengan validasi (DataAnnotations) | `Components/Pages/Register.razor`, `Models/RegistrationModel.cs` |
| State management sesi pengguna | `Services/UserSessionService.cs` |
| Attendance Tracker | `Components/Pages/Attendance.razor` |

## Cara menjalankan

Prasyarat: [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
dotnet run
```

Buka `http://localhost:5080`.

## Ringkasan bantuan Microsoft Copilot

Berikut ringkasan bagaimana Microsoft Copilot membantu pada setiap tahap pengembangan EventEase. Semua saran Copilot saya jalankan dan uji sendiri sebelum dipakai.

1. **Membuat komponen Event Card (Activity 1).** Copilot membantu membuat komponen Blazor `EventCard.razor` dengan field nama, tanggal, lokasi, deskripsi, dan kapasitas. Copilot menyarankan `EditForm` dengan `@bind-Value` untuk *two-way data binding*, serta parameter komponen `Event` dan `EventChanged` (`EventCallback`) agar perubahan data dikirim kembali ke halaman induk. Hasilnya diuji dengan mengubah data event dan memastikan tampilan ikut berubah.
2. **Routing dan debugging (Activity 2).** Copilot membantu menyusun route `/events`, `/events/{Id:int}`, `/register/{EventId:int}`, dan `/attendance`. Saat debugging, Copilot membantu menemukan dua masalah routing: ID event yang tidak ada belum ditangani, dan alamat yang tidak dikenal menampilkan halaman error bawaan browser. Solusinya adalah *route constraint* `:int`, pengecekan `null` yang menampilkan pesan "Event tidak ditemukan", serta halaman `NotFound` di `/not-found` yang dipanggil lewat `UseStatusCodePagesWithReExecute` dan `NotFoundPage` pada `Router`.
3. **Optimasi performa dan validasi (Activity 2).** Copilot menyarankan: `@key` pada daftar event agar render ulang lebih efisien; mengedit pada salinan data (draft) agar tombol Batal tidak mengubah data asli; `@bind:event="oninput"` untuk pencarian langsung; validasi input dengan DataAnnotations (`Required`, `StringLength`, `EmailAddress`, `Range`, `Phone`); serta pengecekan di sisi layanan agar kapasitas tidak berkurang di bawah tiket yang sudah terdaftar, email tidak terdaftar dua kali pada event yang sama, dan jumlah tiket tidak melebihi sisa kursi.
4. **Fitur lanjutan (Activity 3).** Copilot membantu membuat Registration Form dengan `ValidationSummary` dan `ValidationMessage` pada tiap field, state sesi pengguna lewat `UserSessionService` (scoped, dengan event `Changed` agar menu navigasi menampilkan "Halo, nama pengguna" setelah mendaftar), serta Attendance Tracker dengan *checkbox* kehadiran dan ringkasan jumlah serta persentase peserta yang hadir.
5. **Pengujian dan dokumentasi.** Aplikasi dibuild dan dijalankan di .NET 10 (`dotnet run`), lalu diuji manual: validasi form kosong menampilkan pesan error, pendaftaran valid mengurangi sisa kursi, ID event yang tidak ada dan alamat yang tidak dikenal menampilkan halaman yang sesuai. Copilot juga membantu menyusun README ini.

## Struktur proyek

```
EventEase/
├── Components/        # App, Routes, EventCard, Layout, Pages
├── Models/            # EventItem, RegistrationModel, Attendee
├── Services/          # EventService, RegistrationService, UserSessionService
├── wwwroot/app.css
└── Program.cs
```
