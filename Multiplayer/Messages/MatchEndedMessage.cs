using Domain.Interfaces;

namespace Multiplayer.Messages;

public class MatchEndedMessage : ISerializableMessage
{
    public Guid? WinnerId { get; }

    public MatchEndedMessage(Guid? winnerId)
    {
        WinnerId = winnerId;
    }
}
