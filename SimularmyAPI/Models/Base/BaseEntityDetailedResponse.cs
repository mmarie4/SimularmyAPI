namespace SimularmyAPI.Models.Base;

public class BaseEntityDetailedResponse : BaseEntityResponse
{
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}
