using Microsoft.EntityFrameworkCore;
using ProCleanArchitecture.Domain.Enums;
using ProCleanArchitecture.Domain.Interfaces;
using ProCleanArchitecture.Infrastructure.Data;

namespace ProCleanArchitecture.Infrastructure.Repositories;

public class AdminDashboardRepository : IAdminDashboardRepository
{
    private readonly AppDbContext _context;

    public AdminDashboardRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<int> GetProductCountAsync()
    {
        return _context.Products.CountAsync();
    }

    public Task<int> GetActiveProductCountAsync()
    {
        return _context.Products.CountAsync(product => product.Status == ProductStatus.Active);
    }

    public Task<int> GetCategoryCountAsync()
    {
        return _context.Categories.CountAsync();
    }

    public Task<int> GetUserCountAsync()
    {
        return _context.Users.CountAsync();
    }

    public Task<int> GetActiveUserCountAsync()
    {
        return _context.Users.CountAsync(user => user.IsActive);
    }
}
