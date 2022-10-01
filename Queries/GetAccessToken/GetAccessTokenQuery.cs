using Domain.Entities;
using MediatR;

namespace Queries.GetAccessToken;

public class GetAccessTokenQuery : IRequest<(User, string)>
{
    public string Email { get; }
    public string Password { get; }

    public GetAccessTokenQuery(string email,
                               string password)
    {
        Email = email;
        Password = password;
    }
}
