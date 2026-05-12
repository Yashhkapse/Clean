using ProCleanArchitecture.Domain.Enums;

namespace ProCleanArchitecture.Application.Features.Products.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public ProductType Type { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
