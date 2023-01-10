using Domain.Enums;
using Domain.Utils;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Pseudo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public int Elo { get; set; }

        public ICollection<UserUnit> Units { get; set; } = new List<UserUnit>();
    }

    public class UserUnit
    {
        public int UnitId { get; set; }
        public int Count { get; set; }
        public UserUnitLane Lane { get; set; }

        public double Offset
        {
            get
            {
                return Lane switch
                {
                    UserUnitLane.Front => Constants.FRONTLINE_OFFSET_X,
                    UserUnitLane.Middle => Constants.MIDDLE_OFFSET_X,
                    _ => 0,
                };
            }
        }
    }
}
