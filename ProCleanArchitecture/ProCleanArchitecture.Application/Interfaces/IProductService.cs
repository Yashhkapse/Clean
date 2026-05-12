using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProCleanArchitecture.Domain.Entities;


namespace ProCleanArchitecture.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task AddProductAsync(Product product);
    }
}