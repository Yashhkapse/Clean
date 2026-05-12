using AutoMapper;
using FluentValidation;
using MediatR;
using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool IsActive { get; set; } = true;
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IAdminUserRepository _userRepository;

    public CreateUserCommandValidator(IAdminUserRepository userRepository)
    {
        _userRepository = userRepository;

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

    private async Task<bool> BeUniqueEmailAsync(string email, CancellationToken cancellationToken)
    {
        return !await _userRepository.EmailExistsAsync(email);
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IAdminUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IAdminUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        user.Id = Guid.NewGuid();
        user.CreatedAtUtc = DateTime.UtcNow;

        await _userRepository.AddAsync(user);
        return user.Id;
    }
}
