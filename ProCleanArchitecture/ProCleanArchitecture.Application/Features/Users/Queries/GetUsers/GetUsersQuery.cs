using AutoMapper;
using MediatR;
using ProCleanArchitecture.Application.Common.Models;
using ProCleanArchitecture.Application.Features.Users.Dtos;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Users.Queries.GetUsers;

public record GetUsersQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<UserDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResult<UserDto>>
{
    private readonly IAdminUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IAdminUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(request.PageNumber, 1);
        var pageSize = Math.Clamp(request.PageSize, 5, 50);
        var (items, totalCount) = await _userRepository.GetPagedAsync(request.SearchTerm, pageNumber, pageSize);
        var mappedItems = _mapper.Map<IReadOnlyList<UserDto>>(items);

        return new PagedResult<UserDto>(mappedItems, totalCount, pageNumber, pageSize, request.SearchTerm);
    }
}
