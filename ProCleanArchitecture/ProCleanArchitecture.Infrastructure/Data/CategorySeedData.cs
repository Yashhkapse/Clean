using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Infrastructure.Data;

public static class CategorySeedData
{
    public static readonly Guid ElectronicsId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static readonly Guid FashionId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static readonly Guid HomeId = Guid.Parse("33333333-3333-3333-3333-333333333333");
    public static readonly Guid HealthId = Guid.Parse("44444444-4444-4444-4444-444444444444");
    public static readonly Guid SportsId = Guid.Parse("55555555-5555-5555-5555-555555555555");
    public static readonly Guid AccessoriesId = Guid.Parse("66666666-6666-6666-6666-666666666666");
    public static readonly Guid OfficeId = Guid.Parse("77777777-7777-7777-7777-777777777777");
    public static readonly Guid ServicesId = Guid.Parse("88888888-8888-8888-8888-888888888888");
    public static readonly Guid SoftwareId = Guid.Parse("99999999-9999-9999-9999-999999999999");
    public static readonly Guid OthersId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

    public static readonly IReadOnlyList<Category> Categories =
    [
        new Category { Id = ElectronicsId, Name = "Electronics" },
        new Category { Id = FashionId, Name = "Fashion" },
        new Category { Id = HomeId, Name = "Home" },
        new Category { Id = HealthId, Name = "Health" },
        new Category { Id = SportsId, Name = "Sports" },
        new Category { Id = AccessoriesId, Name = "Accessories" },
        new Category { Id = OfficeId, Name = "Office" },
        new Category { Id = ServicesId, Name = "Services" },
        new Category { Id = SoftwareId, Name = "Software" },
        new Category { Id = OthersId, Name = "Others" }
    ];
}
