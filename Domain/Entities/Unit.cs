namespace Domain.Entities;

public class Unit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public UnitStats Stats { get; set; } = new UnitStats();
}

public class UnitStats
{
    public int Damages { get; set; }
    public int Health { get; set; }
    public double Speed { get; set; }
    public double Size { get; set; }
}
