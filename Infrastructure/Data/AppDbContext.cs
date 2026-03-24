using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

// Databaskontext för Identity och våra egna tabeller.
public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
{
    // Tabell för medlemskap.
    public DbSet<Membership> Memberships => Set<Membership>();

    // Tabell för träningspass.
    public DbSet<GymClass> GymClasses => Set<GymClass>();

    // Tabell för bokningar.
    public DbSet<Booking> Bookings => Set<Booking>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}