using System.ComponentModel.DataAnnotations;

namespace SimularmyAPI.Models.Players
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
