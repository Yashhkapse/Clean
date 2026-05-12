using ProCleanArchitecture.Domain.Enums;

namespace ProCleanArchitecture.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ProductStatus Status { get; set; }
    public ProductType Type { get; set; }

    // Foreign Key
    public Guid CategoryId { get; set; }

    // Navigation property (IMPORTANT for EF Core)
    public Category? Category { get; set; }
}