using System.ComponentModel.DataAnnotations;

namespace SimularmyAPI.Models.Players
{
    public class ChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string NewPassword2 { get; set; }


        /// <summary>
        ///     Validates request
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            if (NewPassword != NewPassword2)
            {
                throw new Exception("Passwords don't match");
            }
        }
    }
}
