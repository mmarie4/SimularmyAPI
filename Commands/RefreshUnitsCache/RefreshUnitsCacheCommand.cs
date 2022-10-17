using MediatR;

namespace Commands.RefreshUnitsCache;

public class RefreshUnitsCacheCommand : IRequest
{
    public Guid UserId { get; }

    public RefreshUnitsCacheCommand(Guid userId)
    {
        UserId = userId;
    }
}
