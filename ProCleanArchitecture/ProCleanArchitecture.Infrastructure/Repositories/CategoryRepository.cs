using Microsoft.EntityFrameworkCore;
using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Interfaces;
using ProCleanArchitecture.Infrastructure.Data;

namespace ProCleanArchitecture.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(category => category.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await Task.CompletedTask;
        throw new InvalidOperationException("Categories are managed by the system seed configuration.");
    }
}
