using Bookstore.Application.Abstractions.Authentication;
using Bookstore.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Bookstore.Infrastructure.Authentication;

internal sealed class TokenProvider(IOptions<JwtOptions> jwtOptions)
    : ITokenProvider
{

    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public string CreateAccessToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var algorithm = SecurityAlgorithms.HmacSha512;

        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserDetail.FullName)
            ]),
            Expires = expires,
            SigningCredentials = new SigningCredentials(key, algorithm),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
        };

        var handler = new JsonWebTokenHandler();

        string accessToken = handler.CreateToken(tokenDescriptor);

        return accessToken;
    }
}
