using AutoMapper;
using MediatR;
using ProCleanArchitecture.Application.Features.Users.Dtos;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto?>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IAdminUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IAdminUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        return user is null ? null : _mapper.Map<UserDto>(user);
    }
}
