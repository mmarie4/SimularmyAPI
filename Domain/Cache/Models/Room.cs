using Domain.Interfaces;
using Domain.Utils;

namespace Domain.Cache.Models;

public class Room :  ISerializableMessage
{
    public string Id { get; }
    public PlayerState PlayerA { get; }
    public PlayerState PlayerB { get; }
    public DateTime CreatedOn { get; }

    public Room(string id, PlayerState playerA, PlayerState playerB)
    {
        Id = id;
        PlayerA = playerA;
        PlayerB = playerB;
        CreatedOn = DateTime.Now;
    }

    public bool IsFinished()
        => (DateTime.UtcNow - CreatedOn).Seconds > Constants.MATCH_DURATION_IN_MIN;
}
