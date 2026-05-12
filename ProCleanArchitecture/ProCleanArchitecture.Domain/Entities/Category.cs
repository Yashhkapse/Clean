namespace ProCleanArchitecture.Domain.Entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    // Navigation property (IMPORTANT)
    public ICollection<Product>? Products { get; set; }
}