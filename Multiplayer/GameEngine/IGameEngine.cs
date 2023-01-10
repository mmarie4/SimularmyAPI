using Domain.Cache.Models;

namespace Domain.GameEngine;

public interface IGameEngine
{
    void Update(Room room);
    public Guid? GetWinner(Room room);
}
