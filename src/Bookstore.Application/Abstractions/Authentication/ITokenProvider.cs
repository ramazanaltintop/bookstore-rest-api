using Bookstore.Domain.Users;

namespace Bookstore.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string CreateAccessToken(User user);
}
