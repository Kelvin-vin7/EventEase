using EventEase.Components;
using EventEase.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Data bersama (in-memory) untuk semua pengguna
builder.Services.AddSingleton<EventService>();
builder.Services.AddSingleton<RegistrationService>();
// State per sesi pengguna (satu circuit Blazor)
builder.Services.AddScoped<UserSessionService>();

var app = builder.Build();

// Alamat yang tidak dikenal diarahkan ke halaman /not-found
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
