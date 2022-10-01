namespace Domain.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedById { get; set; }
        public User UpdatedBy { get; set; }

        public void Init(Guid userId)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedById = userId;
            UpdatedAt = DateTime.UtcNow;
            UpdatedById = userId;
        }

        public void Update(Guid userId)
        {
            UpdatedAt = DateTime.UtcNow;
            UpdatedById = userId;
        }
    }
}
