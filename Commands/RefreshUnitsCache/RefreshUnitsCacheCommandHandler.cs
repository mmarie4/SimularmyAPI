using Domain.Repositories.Abstractions;
using MediatR;

namespace Commands.RefreshUnitsCache;

public class RefreshUnitsCacheCommandHandler : IRequestHandler<RefreshUnitsCacheCommand>
{
    private readonly IUnitRepository _unitRepository;
    private readonly IUserRepository _userRepository;

    public RefreshUnitsCacheCommandHandler(IUnitRepository unitRepository, IUserRepository userRepository)
    {
        _unitRepository = unitRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RefreshUnitsCacheCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user?.IsAdmin != true)
            throw new Exception("User is not authorized");

        await _unitRepository.RefreshCache();
        return Unit.Value;
    }
}
