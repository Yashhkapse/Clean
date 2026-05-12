using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> AddAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task<List<Product>> GetAllAsync();
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}