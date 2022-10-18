using Domain.Entities;
using SimularmyAPI.Models.Base;

namespace SimularmyAPI.Models.Army;

public class ArmyResponse : BaseEntityDetailedResponse
{
    public ICollection<UserUnitResponse> UserUnits { get; set; } = new List<UserUnitResponse>();

    public ArmyResponse(Domain.Entities.Army army)
    {
        Id = army.Id;
        UserUnits = new List<UserUnitResponse>();
        foreach (var userUnit in army.UserUnits)
        {
            UserUnits.Add(new UserUnitResponse(userUnit));
        }
        CreatedAt = army.CreatedAt;
        UpdatedAt = army.UpdatedAt;
        CreatedBy = army.CreatedById;
        UpdatedBy = army.UpdatedById;
    }
}
