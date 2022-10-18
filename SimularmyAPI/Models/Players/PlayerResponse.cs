using Domain.Entities;
using SimularmyAPI.Models.Base;

namespace SimularmyAPI.Models.Players
{
    public class PlayerResponse : BaseEntityResponse
    {
        public string? Token { get; }
        public string? Pseudo { get; }
        public string? Email { get; }

        /// <summary>
        ///     Builds a UserResponse from a User entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public PlayerResponse(User entity, string token)
        {
            Id = entity.Id;
            Token = token;
            Email = entity.Email;
            Pseudo = entity.Pseudo;
        }
    }
}
