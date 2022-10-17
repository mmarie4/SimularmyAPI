namespace SimularmyAPI.Models.Base;

public class PaginatedList<T>
{
    public ICollection<T> Values { get; set; } = new List<T>();
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public int Total { get; set; }
}
