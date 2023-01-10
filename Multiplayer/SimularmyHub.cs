using Domain.Cache;
using Microsoft.AspNetCore.SignalR;

namespace Multiplayer;

public class SimularmyHub : Hub
{
    /// <summary>
    ///     Handles player disconnected to remove from the rooms
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;

        UserQueue.Remove(connectionId);

        // TODO: Remove from room and notify in group that a user is disconnected

        return base.OnDisconnectedAsync(exception);
    }

}
