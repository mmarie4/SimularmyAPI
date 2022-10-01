using Domain.Entities;
using MediatR;

namespace Commands.RegisterPlayer;

public class RegisterPlayerCommand : IRequest<(User, string)>
{
    public string Pseudo { get; }
    public string Email { get; }
    public string Password { get; }

    public RegisterPlayerCommand(string pseudo, string email, string password)
    {
        Pseudo = pseudo;
        Email = email;
        Password = password;
    }
}
