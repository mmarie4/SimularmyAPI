using Domain.Entities;

namespace SimularmyAPI.Models.Army;

public class UserUnitResponse
{
    public int UnitId { get; set; }
    public int Count { get; set; }

    public UserUnitResponse(UserUnit userUnit)
    {
        UnitId = userUnit.UnitId;
        Count = userUnit.Count;
    }
}
