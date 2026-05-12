using ProCleanArchitecture.Application.Interfaces;
using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _categoryRepository.AddAsync(category);
    }
}