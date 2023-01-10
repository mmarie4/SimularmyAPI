using Domain.Interfaces;

namespace Multiplayer.Messages;

public class RoomCreatedMessage : ISerializableMessage
{
    public string RoomName { get; }

    public RoomCreatedMessage(string roomName)
    {
        RoomName = roomName;
    }
}
