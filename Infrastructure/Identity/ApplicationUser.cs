using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

// Detta är vår egen användare i systemet
// Vi ärver från IdentityUser för att få inloggning och registrering
public class ApplicationUser : IdentityUser
{
    // Förnamn (extra fält som vi själva lägger till)
    public string FirstName { get; set; } = string.Empty;

    // Efternamn (extra fält)
    public string LastName { get; set; } = string.Empty;
}