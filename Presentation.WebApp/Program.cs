// Här startar vi applikationen.
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.WebApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("CoreFitnessDevDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

// Viktigt: först autentisering, sedan behörighet.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

// Identity sidor.
app.MapRazorPages();

app.Run();
