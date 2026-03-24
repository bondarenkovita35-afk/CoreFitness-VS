namespace Domain.Common;

// Eget fel för regler i domänen.
public sealed class DomainException : Exception
{
    // Skapar ett nytt domänfel med ett tydligt meddelande.
    public DomainException(string message) : base(message)
    {
    }
}