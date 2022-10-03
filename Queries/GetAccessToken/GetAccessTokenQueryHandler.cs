using Domain.Entities;
using Domain.Options;
using Domain.Repositories.Abstractions;
using Domain.Utils;
using MediatR;
using Microsoft.Extensions.Options;

namespace Queries.GetAccessToken;

public class GetAccessTokenQueryHandler : IRequestHandler<GetAccessTokenQuery, (User, string)>
{
    private readonly IUserRepository _userRepository;
    private readonly IOptions<SecurityOptions> _securityOptions;

    public GetAccessTokenQueryHandler(IUserRepository userRepository,
                                      IOptions<SecurityOptions> securityOptions)
    {
        _userRepository = userRepository;
        _securityOptions = securityOptions;
    }

    public async Task<(User, string)> Handle(GetAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception($"User not found with email {request.Email}");
        }

        CryptoHelpers.CheckPassword(request.Password, user);

        var token = await Task.Run(() => CryptoHelpers.GenerateToken(user,
                                                                     _securityOptions.Value.Secret,
                                                                     _securityOptions.Value.Issuer,
                                                                     _securityOptions.Value.Audience));

        return (user, token);
    }
}
