namespace ProCleanArchitecture.Domain.Interfaces;

public interface IAdminDashboardRepository
{
    Task<int> GetProductCountAsync();
    Task<int> GetActiveProductCountAsync();
    Task<int> GetCategoryCountAsync();
    Task<int> GetUserCountAsync();
    Task<int> GetActiveUserCountAsync();
}
