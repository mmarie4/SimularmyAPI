using MediatR;

namespace Commands.DeleteAccount;

public class DeleteAccountCommand : IRequest
{
    public DeleteAccountCommand(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }


}
