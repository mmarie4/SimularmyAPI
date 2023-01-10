using Domain.Interfaces;

namespace Domain.Cache.Models;

public class PlayerState
{
    public Guid Id { get; }
    public string ConnectionId { get; }
    public IEnumerable<InGameUnit> Units { get; }

    public PlayerState(Guid id, string connectionId, IEnumerable<InGameUnit> units)
    {
        Id = id;
        ConnectionId = connectionId;
        Units = units;
    }
}
