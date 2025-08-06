using System.Security.Claims;

namespace Bookstore.Infrastructure.Authentication;
internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out Guid parsedUserId)
            ? parsedUserId
            : Guid.Empty;
    }

    public static string GetFullName(this ClaimsPrincipal? principal)
    {
        string? fullName = principal?.FindFirst(ClaimTypes.Name)?.Value;

        if (fullName is null)
            return string.Empty;

        return fullName;
    }
}