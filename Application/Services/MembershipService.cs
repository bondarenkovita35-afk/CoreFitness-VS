using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

// Service som hanterar medlemskap.
public class MembershipService
{
    private readonly IAppDbContext _context;

    public MembershipService(IAppDbContext context)
    {
        _context = context;
    }

    // Hämtar aktivt medlemskap för en användare.
    public async Task<Membership?> GetActiveMembershipAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Memberships
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, cancellationToken);
    }

    // Skapar ett nytt medlemskap om användaren inte redan har ett aktivt.
    public async Task<Result<Membership>> CreateMembershipAsync(string userId, CancellationToken cancellationToken = default)
    {
        var existingMembership = await _context.Memberships
            .FirstOrDefaultAsync(x => x.UserId == userId && x.IsActive, cancellationToken);

        if (existingMembership is not null)
        {
            return Result<Membership>.Failure("Du har redan ett aktivt medlemskap.");
        }

        var createResult = Membership.Create(userId);

        if (!createResult.IsSuccess || createResult.Value is null)
        {
            return Result<Membership>.Failure(createResult.Error ?? "Kunde inte skapa medlemskap.");
        }

        _context.Memberships.Add(createResult.Value);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Membership>.Success(createResult.Value);
    }
}