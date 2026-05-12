using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private static readonly List<Product> _products = new();

    public Task<Product> AddAsync(Product product)
    {
        _products.Add(product);
        return Task.FromResult(product);
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_products.FirstOrDefault(x => x.Id == id));
    }

    public Task<List<Product>> GetAllAsync()
    {
        return Task.FromResult(_products);
    }

    public Task UpdateAsync(Product product)
    {
        var index = _products.FindIndex(x => x.Id == product.Id);
        if (index != -1) _products[index] = product;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        var item = _products.FirstOrDefault(x => x.Id == id);
        if (item != null) _products.Remove(item);
        return Task.CompletedTask;
    }
}