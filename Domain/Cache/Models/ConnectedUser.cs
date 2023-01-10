using Domain.Entities;

namespace Domain.Cache.Models;

public class ConnectedUser
{
    public string ConnectionId { get; private set; }
    public User User { get; private set; }

    public ConnectedUser(string connectionId, User user)
    {
        ConnectionId = connectionId;
        User = user;
    }
}
