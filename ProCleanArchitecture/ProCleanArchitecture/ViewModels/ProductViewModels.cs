using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProCleanArchitecture.Domain.Enums;

namespace ProCleanArchitecture.Web.ViewModels;

public class ProductListItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public ProductType Type { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

public class ProductDetailsViewModel
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

public class ProductFormViewModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Range(0, 999999999)]
    public decimal Price { get; set; }

    public ProductStatus Status { get; set; } = ProductStatus.Active;
    public ProductType Type { get; set; } = ProductType.Physical;

    [Display(Name = "Category")]
    [Required]
    public Guid CategoryId { get; set; }

    public IEnumerable<SelectListItem> Categories { get; set; } = Array.Empty<SelectListItem>();
}
