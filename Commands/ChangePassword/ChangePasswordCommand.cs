using MediatR;

namespace Commands.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public Guid UserId { get; }
    public string OldPassword { get; }
    public string NewPassword { get; }

    public ChangePasswordCommand(string oldPassword, string newPassword, Guid userId)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
        UserId = userId;
    }
}
