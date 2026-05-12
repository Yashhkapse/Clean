using AutoMapper;
using ProCleanArchitecture.Application.Features.Dashboard.Dtos;
using ProCleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using ProCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;
using ProCleanArchitecture.Application.Features.Products.Dtos;
using ProCleanArchitecture.Application.Features.Users.Commands.CreateUser;
using ProCleanArchitecture.Application.Features.Users.Commands.UpdateUser;
using ProCleanArchitecture.Application.Features.Users.Dtos;
using ProCleanArchitecture.Web.ViewModels;

namespace ProCleanArchitecture.Web.Mapping;

public class WebMappingProfile : Profile
{
    public WebMappingProfile()
    {
        CreateMap<DashboardStatsDto, AdminDashboardViewModel>();

        CreateMap<ProductDto, ProductListItemViewModel>();
        CreateMap<ProductDto, ProductDetailsViewModel>();
        CreateMap<ProductDto, ProductFormViewModel>();
        CreateMap<ProductFormViewModel, CreateProductCommand>();
        CreateMap<ProductFormViewModel, UpdateProductCommand>();

        CreateMap<UserDto, UserListItemViewModel>();
        CreateMap<UserDto, UserDetailsViewModel>();
        CreateMap<UserDto, UserFormViewModel>();
        CreateMap<UserFormViewModel, CreateUserCommand>();
        CreateMap<UserFormViewModel, UpdateUserCommand>();
    }
}


