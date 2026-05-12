using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Domain.Interfaces;

public interface IAdminProductRepository
{
    Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedAsync(string? searchTerm, int pageNumber, int pageSize);
    Task<Product?> GetByIdAsync(Guid id);
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Product product);
    Task<int> CountAsync();
    Task<int> CountActiveAsync();
}
