using Domain.Repositories.Abstractions;
using Domain.Utils;
using MediatR;

namespace Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly IUserRepository _userRepository;
    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        CryptoHelpers.CheckPassword(request.OldPassword, user);

        user.PasswordHash = CryptoHelpers.HashUsingPbkdf2(request.NewPassword, user.PasswordSalt.ToString());

        var result = await _userRepository.Update(user, user.Id);
        await _userRepository.SaveAsync();

        return Unit.Value;
    }
}
