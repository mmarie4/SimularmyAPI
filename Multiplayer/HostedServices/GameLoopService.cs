using Domain.Cache;
using Domain.GameEngine;
using Domain.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Multiplayer.Messages;

namespace Multiplayer.HostedServices;

public class GameLoopService : IHostedService
{
    private ILogger<GameLoopService> _logger;
    private Timer _timer;
    private TimeSpan TickDelay = TimeSpan.FromMilliseconds(33);
    private readonly IGameEngine _gameEngine;

    private readonly IHubContext<SimularmyHub> _hubContext;

    public GameLoopService(ILogger<GameLoopService> logger,
                           IHubContext<SimularmyHub> hubContext,
                           IGameEngine gameEngine)
    {
        _logger = logger;
        _hubContext = hubContext;
        _gameEngine = gameEngine;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(Broadcast, null, TickDelay, TickDelay);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    /// <summary>
    ///     For each room, broadcast message for game state
    /// </summary>
    /// <returns></returns>
    private async void Broadcast(object state)
    {
        foreach (var room in RoomsStore.Rooms)
        {
            if (room.IsFinished())
            {
                var winnerId = _gameEngine.GetWinner(room);

                var matchEndedMessage = new MatchEndedMessage(winnerId);
                await _hubContext.Clients.Group(room.Id.ToString()).SendAsync(matchEndedMessage.ToMessage());

                // TODO: Update Elo
            }
            else
            {
                _gameEngine.Update(room);
                await _hubContext.Clients.Group(room.Id.ToString()).SendAsync(room.ToMessage());
            }
        }
    }
}
