using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Common.Interfaces;

// Interface för databaskontexten.
// Gör att Application-lagret inte beror direkt på Infrastructure.
public interface IAppDbContext
{
    DbSet<Membership> Memberships { get; }
    DbSet<GymClass> GymClasses { get; }
    DbSet<Booking> Bookings { get; }

    // Sparar ändringar i databasen.
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}