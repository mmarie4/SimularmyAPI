using Domain.Entities;
using MediatR;

namespace Queries.GetPlayerByEmail;

public class GetPlayerByEmailQuery : IRequest<User?>
{
    public string Email { get; }

    public GetPlayerByEmailQuery(string email)
    {
        Email = email;
    }
}
