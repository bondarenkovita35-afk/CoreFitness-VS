using Application.Common.Interfaces;
using Application.Services;
using Infrastructure.Data; // vår databas
using Infrastructure.Identity; // vår användare
using Microsoft.AspNetCore.Identity; // Identity
using Microsoft.EntityFrameworkCore; // EF Core

var builder = WebApplication.CreateBuilder(args);

// Här använder vi InMemory-databas under utveckling.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CoreFitnessDevDb"));

builder.Services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<MembershipService>();

// Lägger till Identity med standardsidor.
builder.Services
    .AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredLength = 6;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// MVC
builder.Services.AddControllersWithViews();

// Viktigt för Identity UI (Razor Pages)
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Viktigt: måste vara med för login.
app.UseAuthentication();
app.UseAuthorization();

// Vanlig route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity sidor
app.MapRazorPages();

// Skapar testdata när appen startar.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await DbSeeder.SeedAsync(context);
}

app.Run();