using AutoMapper;
using ProCleanArchitecture.Application.Features.Categories.Dtos;
using ProCleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using ProCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using ProCleanArchitecture.Application.Features.Products.Dtos;
using ProCleanArchitecture.Application.Features.Users.Commands.CreateUser;
using ProCleanArchitecture.Application.Features.Users.Commands.UpdateUser;
using ProCleanArchitecture.Application.Features.Users.Dtos;
using ProCleanArchitecture.Domain.Entities;

namespace ProCleanArchitecture.Application.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<Category, CategoryLookupDto>();

        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : string.Empty));
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}
