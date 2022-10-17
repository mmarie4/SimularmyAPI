using Domain.Entities;
using SimularmyAPI.Models.Base;

namespace SimularmyAPI.Models.Units;

public class UnitCollectionResponse : PaginatedList<UnitResponse>
{
    public UnitCollectionResponse(ICollection<Unit> units,
                                  int? limit,
                                  int? offset,
                                  int totalCount)
    {
        Values = new List<UnitResponse>();
        foreach (var unit in units)
        {
            Values.Add(new UnitResponse(unit));
        }

        Limit = limit;
        Offset = offset;
        Total = totalCount;
    }
}
