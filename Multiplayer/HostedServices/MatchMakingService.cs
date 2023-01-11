using Domain.Cache;
using Domain.Cache.Models;
using Domain.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Multiplayer.Messages;
using Multiplayer;
using System.ComponentModel.DataAnnotations;

namespace Multiplayer.HostedServices;

public class MatchMakingService : IHostedService
{
    private Timer _timer;
    private TimeSpan TickDelay = TimeSpan.FromMilliseconds(1000);
    private readonly ILogger<MatchMakingService> _logger;

    private readonly IHubContext<SimularmyHub> _hubContext;

    public MatchMakingService(IHubContext<SimularmyHub> hubContext, ILogger<MatchMakingService>)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(CreateRooms, null, TickDelay, TickDelay);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public async void CreateRooms(object state)
    {
        _logger.LogDebug("Matchmaking: {nbPlayers} found in queue", UserQueue.Count);

        foreach (var user in UserQueue.Users)
        {
            if (user.ConnectionId is null || user.User is null)
                continue;

            var opponent = UserQueue.FindClosest(user);
            if (opponent is null)
                continue;

            var groupName = $"{user.User.Id}-{opponent.User.Id}";
            await _hubContext.Groups.AddToGroupAsync(user.ConnectionId, groupName);
            var roomCreatedMessage = new RoomCreatedMessage(groupName);
            await _hubContext.Clients
                .Users(new List<string>() { user.ConnectionId, opponent.ConnectionId })
                .SendAsync(roomCreatedMessage.ToMessage());

            RoomsStore.AddRoom(groupName,
                               new PlayerState(user.User.Id, user.ConnectionId, InGameUnit.InitFromUserArmy(user.User.Units, 0)),
                               new PlayerState(user.User.Id, user.ConnectionId, InGameUnit.InitFromUserArmy(opponent.User.Units, Constants.MAX_POS_X)));

            _logger.LogInformation("Match created: {groupName}", groupName);
        }
    }
}
