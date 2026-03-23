using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

// Detta är vår databas-kontext
// Här kopplas databasen till våra modeller
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    // Konstruktor som tar emot inställningar (connection string)
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Här kommer vi senare lägga till våra tabeller:
    // Membership, Classes, Bookings
}