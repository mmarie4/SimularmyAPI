using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace SimularmyAPI.Models.Players
{
    public class SignUpRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Pseudo { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Password2 { get; set; }

        /// <summary>
        ///     Validates request
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            var email = new MailAddress(Email.Trim());
            if (string.IsNullOrWhiteSpace(email.Address))
            {
                throw new Exception("Invalid email format");
            }

            if (Password != Password2)
            {
                throw new Exception("Passwords don't match");
            }
        }
    }
}
