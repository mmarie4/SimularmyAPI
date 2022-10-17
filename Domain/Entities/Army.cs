namespace Domain.Entities;

public class Army : BaseEntity
{
    public ICollection<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
}

public record UserUnit
{
    public int UnitId { get; set; }
    public int Count { get; set; }
}
