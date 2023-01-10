using Domain.Cache;
using Domain.Cache.Models;
using Domain.Infrastructure;
using Domain.Repositories.Abstractions;
using MediatR;
using Unit = MediatR.Unit;

namespace Commands.JoinMatchmakingQueue;

public class JoinMatchmakingQueueCommandHandler : IRequestHandler<JoinMatchmakingQueueCommand>
{
    private readonly IUserRepository _userRepository;

    public JoinMatchmakingQueueCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<Unit> Handle(JoinMatchmakingQueueCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
            throw new DomainException(404, $"User not found with id {request.UserId}");

        UserQueue.Add(new ConnectedUser(request.ConnectionId, user));

        return Unit.Value;
    }
}
