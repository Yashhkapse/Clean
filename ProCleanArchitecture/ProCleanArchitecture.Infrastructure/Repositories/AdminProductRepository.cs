using Microsoft.EntityFrameworkCore;
using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Enums;
using ProCleanArchitecture.Domain.Interfaces;
using ProCleanArchitecture.Infrastructure.Data;

namespace ProCleanArchitecture.Infrastructure.Repositories;

public class AdminProductRepository : IAdminProductRepository
{
    private readonly AppDbContext _context;

    public AdminProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(IReadOnlyList<Product> Items, int TotalCount)> GetPagedAsync(string? searchTerm, int pageNumber, int pageSize)
    {
        var query = _context.Products
            .Include(product => product.Category)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var normalizedSearch = searchTerm.Trim();
            query = query.Where(product =>
                product.Name.Contains(normalizedSearch) ||
                product.Description.Contains(normalizedSearch) ||
                (product.Category != null && product.Category.Name.Contains(normalizedSearch)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(product => product.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(product => product.Category)
            .FirstOrDefaultAsync(product => product.Id == id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Products.CountAsync();
    }

    public async Task<int> CountActiveAsync()
    {
        return await _context.Products.CountAsync(product => product.Status == ProductStatus.Active);
    }
}
