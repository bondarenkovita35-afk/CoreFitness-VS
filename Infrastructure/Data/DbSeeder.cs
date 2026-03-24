using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

// Lägger in testdata i databasen.
public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Om det redan finns pass, gör inget.
        if (await context.GymClasses.AnyAsync())
        {
            return;
        }

        var yoga = GymClass.Create(
            "Yoga",
            DateTime.UtcNow.AddDays(1).Date.AddHours(18),
            "Anna",
            15);

        var spinning = GymClass.Create(
            "Spinning",
            DateTime.UtcNow.AddDays(2).Date.AddHours(17),
            "Erik",
            12);

        var strength = GymClass.Create(
            "Styrketräning",
            DateTime.UtcNow.AddDays(3).Date.AddHours(19),
            "Maria",
            10);

        if (yoga.IsSuccess && yoga.Value is not null)
            context.GymClasses.Add(yoga.Value);

        if (spinning.IsSuccess && spinning.Value is not null)
            context.GymClasses.Add(spinning.Value);

        if (strength.IsSuccess && strength.Value is not null)
            context.GymClasses.Add(strength.Value);

        await context.SaveChangesAsync();
    }
}