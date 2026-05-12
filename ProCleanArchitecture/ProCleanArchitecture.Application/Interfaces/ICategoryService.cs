using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllAsync();

    Task AddAsync(Category category);
}