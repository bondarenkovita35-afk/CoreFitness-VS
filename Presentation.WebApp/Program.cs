// Här startar vi applikationen.
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Här väljer vi databas beroende på miljö.
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("CoreFitnessDevDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// Här lägger vi till Identity för inloggning.
builder.Services
    .AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Lägg till MVC och Razor Pages.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Här sätter vi middleware.
app.UseHttpsRedirection();
app.UseRouting();

// Viktigt: först autentisering, sedan behörighet.
app.UseAuthentication();
app.UseAuthorization();

// Routing till controllers.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Identity sidor.
app.MapRazorPages();

app.Run();