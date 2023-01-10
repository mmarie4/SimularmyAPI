using Domain.Cache.Models;

namespace Domain.Cache;

public static class RoomsStore
{
    private static ICollection<Room> _rooms = new List<Room>();
    
    public static ICollection<Room> Rooms => _rooms;

    public static void AddRoom(string roomId, PlayerState playerA, PlayerState playerB)
    {
        _rooms.Add(new Room(roomId, playerA, playerB));
    }

    public static void DeleteRoom(string roomId)
    {
        _rooms.Remove(_rooms.First(x => x.Id == roomId));
    }
}
