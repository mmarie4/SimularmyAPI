using Domain.Entities;
using HeroicBrawlServer.DAL.Repositories.Abstractions;
using MediatR;

namespace Queries.GetPlayerByEmail;

public class GetPlayerByEmailQueryHandler : IRequestHandler<GetPlayerByEmailQuery, User?>
{
    private readonly IUserRepository _userRepository;
    public GetPlayerByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetPlayerByEmailQuery request,
                                    CancellationToken cancellationToken)
    {
        return await _userRepository.GetByEmailAsync(request.Email);
    }
}
