using Domain.Common;

namespace Domain.Entities;

// Representerar ett träningspass.
public class GymClass : BaseEntity
{
    // Namn på passet.
    public string Name { get; private set; }

    // Datum och tid för passet.
    public DateTime Date { get; private set; }

    // Instruktör.
    public string Instructor { get; private set; }

    // Max antal deltagare.
    public int Capacity { get; private set; }

    // Lista med bokningar (userId).
    private readonly List<string> _bookedUsers = new();

    public IReadOnlyCollection<string> BookedUsers => _bookedUsers.AsReadOnly();

    private GymClass(string name, DateTime date, string instructor, int capacity)
    {
        Name = name;
        Date = date;
        Instructor = instructor;
        Capacity = capacity;
    }

    // Skapar ett nytt pass.
    public static Result<GymClass> Create(string name, DateTime date, string instructor, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<GymClass>.Failure("Namn saknas.");

        if (capacity <= 0)
            return Result<GymClass>.Failure("Kapacitet måste vara större än 0.");

        var gymClass = new GymClass(name, date, instructor, capacity);

        return Result<GymClass>.Success(gymClass);
    }

    // Bokar en användare till passet.
    public Result Book(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return Result.Failure("UserId saknas.");

        // Kontroll: redan bokad
        if (_bookedUsers.Contains(userId))
            return Result.Failure("Du har redan bokat detta pass.");

        // Kontroll: fullt
        if (_bookedUsers.Count >= Capacity)
            return Result.Failure("Passet är fullbokat.");

        _bookedUsers.Add(userId);

        return Result.Success();
    }

    // Avbokning
    public Result CancelBooking(string userId)
    {
        if (!_bookedUsers.Contains(userId))
            return Result.Failure("Bokning finns inte.");

        _bookedUsers.Remove(userId);

        return Result.Success();
    }
}