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

1. **Event Card (Activity 1).** Saya meminta Copilot membuat komponen Blazor `EventCard` dengan field nama, tanggal, lokasi, deskripsi, dan kapasitas. Copilot menyarankan `EditForm` dengan `@bind-Value` untuk *two-way data binding* serta parameter komponen `Event` dan `EventChanged` agar perubahan dikirim ke komponen induk.
2. **Routing dan debugging (Activity 2).** Copilot membantu menambah route `/events/{Id:int}` dan `/register/{EventId:int}`, serta menemukan penyebab error routing, yaitu parameter yang tidak cocok dan halaman yang tidak menangani ID yang tidak ada. Solusinya: *route constraint* `:int`, pengecekan `null`, dan halaman `NotFound` di `Routes.razor`.
3. **Optimasi performa dan validasi.** Copilot menyarankan: `@key` pada daftar agar render ulang efisien, mengedit pada salinan data (draft) agar tombol Batal tidak mengubah data asli, `@bind:event="oninput"` untuk pencarian langsung, validasi input memakai DataAnnotations (`Required`, `EmailAddress`, `Range`, `Phone`), serta pengecekan sisa kursi dan email ganda di sisi layanan.
4. **Fitur lanjutan (Activity 3).** Copilot membantu membuat Registration Form dengan `ValidationMessage`, `UserSessionService` (scoped, dengan event `Changed` agar menu navigasi ikut diperbarui), dan Attendance Tracker dengan *checkbox* kehadiran dan ringkasan persentase hadir.
5. **Dokumentasi.** Copilot membantu menyusun README ini dan ringkasan langkah pengembangan.

## Struktur proyek

```
EventEase/
├── Components/        # App, Routes, EventCard, Layout, Pages
├── Models/            # EventItem, RegistrationModel, Attendee
├── Services/          # EventService, RegistrationService, UserSessionService
├── wwwroot/app.css
└── Program.cs
```
