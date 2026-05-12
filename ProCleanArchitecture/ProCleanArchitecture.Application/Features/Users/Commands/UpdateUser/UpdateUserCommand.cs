using AutoMapper;
using FluentValidation;
using MediatR;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool IsActive { get; set; }
}

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IAdminUserRepository _userRepository;

    public UpdateUserCommandValidator(IAdminUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(user => user.Id).NotEmpty();
        RuleFor(user => user.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(user => user.LastName).NotEmpty().MaximumLength(100);
        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(256)
            .MustAsync(BeUniqueEmailAsync)
            .WithMessage("A user with this email already exists.");
        RuleFor(user => user.Role).NotEmpty().MaximumLength(100);
    }

    private async Task<bool> BeUniqueEmailAsync(UpdateUserCommand command, string email, CancellationToken cancellationToken)
    {
        return !await _userRepository.EmailExistsAsync(email, command.Id);
    }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IAdminUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IAdminUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user is null)
        {
            return false;
        }

        var createdAtUtc = user.CreatedAtUtc;
        _mapper.Map(request, user);
        user.CreatedAtUtc = createdAtUtc;
        user.UpdatedAtUtc = DateTime.UtcNow;

        await _userRepository.UpdateAsync(user);
        return true;
    }
}
