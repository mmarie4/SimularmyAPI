using Domain.Entities;
using Domain.Options;
using Domain.Repositories.Abstractions;
using Domain.Utils;
using MediatR;
using Microsoft.Extensions.Options;

namespace Commands.RegisterPlayer;

public class RegisterPlayerCommandHandler : IRequestHandler<RegisterPlayerCommand, (User, string)>
{
    private readonly IUserRepository _userRepository;
    private readonly IOptions<SecurityOptions> _securityOptions;
    public RegisterPlayerCommandHandler(IUserRepository userRepository,
                                        IOptions<SecurityOptions> securityOptions)
    {
        _userRepository = userRepository;
        _securityOptions = securityOptions;
    }

    public async Task<(User, string)> Handle(RegisterPlayerCommand request,
                                   CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            throw new Exception("Email already used");
        }

        var salt = CryptoHelpers.GenerateSalt();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            Pseudo = request.Pseudo,
            PasswordSalt = salt.ToString(),
            PasswordHash = CryptoHelpers.HashUsingPbkdf2(request.Password, salt.ToString())
        };
        user.Init(user.Id);

        var token = await Task.Run(() => CryptoHelpers.GenerateToken(user,
                                                                     _securityOptions.Value.Secret,
                                                                     _securityOptions.Value.Issuer,
                                                                     _securityOptions.Value.Audience));

        var result = await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();

        return (result, token);
    }
}
