using Microsoft.EntityFrameworkCore;
using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Interfaces;
using ProCleanArchitecture.Infrastructure.Data;

namespace ProCleanArchitecture.Infrastructure.Repositories;

public class AdminUserRepository : IAdminUserRepository
{
    private readonly AppDbContext _context;

    public AdminUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<(IReadOnlyList<User> Items, int TotalCount)> GetPagedAsync(string? searchTerm, int pageNumber, int pageSize)
    {
        var query = _context.Users.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var normalizedSearch = searchTerm.Trim();
            query = query.Where(user =>
                user.FirstName.Contains(normalizedSearch) ||
                user.LastName.Contains(normalizedSearch) ||
                user.Email.Contains(normalizedSearch) ||
                user.Role.Contains(normalizedSearch));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(user => user.LastName)
            .ThenBy(user => user.FirstName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EmailExistsAsync(string email, Guid? excludedUserId = null)
    {
        var normalizedEmail = email.Trim().ToLower();

        return await _context.Users.AnyAsync(user =>
            user.Email.ToLower() == normalizedEmail &&
            (!excludedUserId.HasValue || user.Id != excludedUserId.Value));
    }

    public async Task<int> CountAsync()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<int> CountActiveAsync()
    {
        return await _context.Users.CountAsync(user => user.IsActive);
    }
}
