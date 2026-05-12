using MediatR;
using ProCleanArchitecture.Application.Features.Dashboard.Dtos;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Dashboard.Queries.GetDashboardStats;

public record GetDashboardStatsQuery : IRequest<DashboardStatsDto>;

public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
{
    private readonly IAdminDashboardRepository _dashboardRepository;

    public GetDashboardStatsQueryHandler(IAdminDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
    {
        return new DashboardStatsDto
        {
            TotalProducts = await _dashboardRepository.GetProductCountAsync(),
            ActiveProducts = await _dashboardRepository.GetActiveProductCountAsync(),
            TotalCategories = await _dashboardRepository.GetCategoryCountAsync(),
            TotalUsers = await _dashboardRepository.GetUserCountAsync(),
            ActiveUsers = await _dashboardRepository.GetActiveUserCountAsync()
        };
    }
}
