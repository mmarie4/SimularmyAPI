using MediatR;

namespace Commands.JoinMatchmakingQueue;

public class JoinMatchmakingQueueCommand : IRequest
{
    public Guid UserId { get; }
    public string ConnectionId { get; }

    public JoinMatchmakingQueueCommand(Guid userId, string connectionId)
    {
        UserId = userId;
        ConnectionId = connectionId;
    }
}
