namespace Multiplayer.GameEngine.Models;

public class Target
{
    public Guid Id { get; }
    public double Distance { get; }
    public double PosX { get; }
    public double PosY { get; }

    public Target(Guid id, double distance, double posX, double posY)
    {
        Id = id;
        Distance = distance;
        PosX = posX;
        PosY = posY;
    }
}
