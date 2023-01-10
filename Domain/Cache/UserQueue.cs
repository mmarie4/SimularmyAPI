using Domain.Cache.Models;

namespace Domain.Cache;

public static class UserQueue
{
    private static ICollection<ConnectedUser> _users = new List<ConnectedUser>();

    public static ICollection<ConnectedUser> Users => _users;
    public static int Count => _users.Count;

    public static void Add(ConnectedUser user)
        => _users.Add(user);

    public static void Remove(string connectionId)
    {
        var user = _users.FirstOrDefault(x => x.ConnectionId == connectionId);
        if (user is not null)
            _users.Remove(user);
    }

    public static ConnectedUser? FindClosest(ConnectedUser user)
        => _users.Where(u => u.User.Id != user.User.Id && u.User.Elo < user.User.Elo)
                 .OrderBy(u => u.User.Elo)
                 .FirstOrDefault();
}
