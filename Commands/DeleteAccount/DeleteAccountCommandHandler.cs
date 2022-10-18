using Domain.Infrastructure;
using Domain.Repositories.Abstractions;
using MediatR;

namespace Commands.DeleteAccount;

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteAccountCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
            throw new DomainException(400, $"User not found with id {request.UserId}");

        await _userRepository.Remove(user);
        await _userRepository.SaveAsync();

        return Unit.Value;
    }
}
