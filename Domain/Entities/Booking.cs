using Domain.Common;

namespace Domain.Entities;

// Representerar en bokning av ett pass.
public class Booking : BaseEntity
{
    // Id för användaren som bokar.
    public string UserId { get; private set; }

    // Id för passet som bokas.
    public Guid GymClassId { get; private set; }

    // Visar om bokningen är aktiv.
    public bool IsActive { get; private set; }

    private Booking(string userId, Guid gymClassId)
    {
        UserId = userId;
        GymClassId = gymClassId;
        IsActive = true;
    }

    // Skapar en ny bokning.
    public static Result<Booking> Create(string userId, Guid gymClassId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Result<Booking>.Failure("UserId saknas.");
        }

        if (gymClassId == Guid.Empty)
        {
            return Result<Booking>.Failure("GymClassId saknas.");
        }

        var booking = new Booking(userId, gymClassId);

        return Result<Booking>.Success(booking);
    }

    // Avbokar en aktiv bokning.
    public Result Cancel()
    {
        if (!IsActive)
        {
            return Result.Failure("Bokningen är redan avbokad.");
        }

        IsActive = false;

        return Result.Success();
    }
}