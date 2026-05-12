using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProCleanArchitecture.Application.Interfaces;
using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _repository.AddAsync(product);
        }
    }
}