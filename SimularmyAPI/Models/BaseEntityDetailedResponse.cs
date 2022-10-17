﻿namespace SimularmyAPI.Models;

public class BaseEntityDetailedResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}
