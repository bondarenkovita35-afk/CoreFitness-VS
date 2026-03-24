namespace Domain.Common;

// Basklass för alla entiteter i systemet.
public abstract class BaseEntity
{
    // Unikt id för varje objekt.
    public Guid Id { get; protected set; } = Guid.NewGuid();

    // Tid när objektet skapades.
    public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
}