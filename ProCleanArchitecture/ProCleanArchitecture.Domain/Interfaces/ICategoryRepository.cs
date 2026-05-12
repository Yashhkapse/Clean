using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Domain.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();

    Task AddAsync(Category category);
}