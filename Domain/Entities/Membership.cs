using Domain.Common;

namespace Domain.Entities;

// Representerar ett medlemskap i gymmet.
public class Membership : BaseEntity
{
    // Koppling till användare (Identity).
    public string UserId { get; private set; }

    // Visar om medlemskapet är aktivt.
    public bool IsActive { get; private set; }

    // Startdatum för medlemskapet.
    public DateTime StartDate { get; private set; }

    // Slutdatum för medlemskapet.
    public DateTime EndDate { get; private set; }

    private Membership(string userId, DateTime startDate, DateTime endDate)
    {
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = true;
    }

    // Skapar ett nytt medlemskap.
    public static Result<Membership> Create(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Result<Membership>.Failure("UserId saknas.");
        }

        var start = DateTime.UtcNow;
        var end = start.AddMonths(1);

        var membership = new Membership(userId, start, end);

        return Result<Membership>.Success(membership);
    }

    // Avslutar medlemskapet.
    public Result Cancel()
    {
        if (!IsActive)
        {
            return Result.Failure("Medlemskapet är redan avslutat.");
        }

        IsActive = false;

        return Result.Success();
    }
}