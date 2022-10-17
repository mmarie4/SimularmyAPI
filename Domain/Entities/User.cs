namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Pseudo { get; set; }
        public string Email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public bool   IsAdmin { get; set; }
    }
}
