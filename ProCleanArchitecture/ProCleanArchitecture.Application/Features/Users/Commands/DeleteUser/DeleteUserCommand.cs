using MediatR;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IAdminUserRepository _userRepository;

    public DeleteUserCommandHandler(IAdminUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
        {
            return false;
        }

        await _userRepository.DeleteAsync(user);
        return true;
    }
}
