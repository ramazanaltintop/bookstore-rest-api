using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Bookstore.Infrastructure.Authentication;

internal sealed class JwtConfigureOptions(IConfiguration configuration)
    : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {
        configuration.GetSection("Jwt").Bind(options);
    }
}
