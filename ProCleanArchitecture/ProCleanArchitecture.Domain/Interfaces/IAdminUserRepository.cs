using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Domain.Interfaces;

public interface IAdminUserRepository
{
    Task<(IReadOnlyList<User> Items, int TotalCount)> GetPagedAsync(string? searchTerm, int pageNumber, int pageSize);
    Task<User?> GetByIdAsync(Guid id);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<bool> EmailExistsAsync(string email, Guid? excludedUserId = null);
    Task<int> CountAsync();
    Task<int> CountActiveAsync();
}
