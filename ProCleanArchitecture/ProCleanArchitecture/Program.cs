using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProCleanArchitecture.Application.Common.Behaviors;
using ProCleanArchitecture.Application.Features.Products.Commands.CreateProduct;
using ProCleanArchitecture.Application.Features.Products.Queries.GetProducts;
using ProCleanArchitecture.Application.Interfaces;
using ProCleanArchitecture.Application.Mappings;
using ProCleanArchitecture.Application.Services;
using ProCleanArchitecture.Domain.Interfaces;
using ProCleanArchitecture.Infrastructure.Data;
using ProCleanArchitecture.Infrastructure.Repositories;
using ProCleanArchitecture.Web.Mapping;

var builder = WebApplication.CreateBuilder(args);

// ======================
// MVC
// ======================
builder.Services.AddControllersWithViews();

// ======================
// DB CONTEXT (VERY IMPORTANT - YOU WERE MISSING THIS)
// ======================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// ======================
// APPLICATION SERVICES
// ======================
builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(typeof(GetProductsQuery).Assembly));

var autoMapperLicenseKey = builder.Configuration["AutoMapper:LicenseKey"];
builder.Services.AddAutoMapper(configuration =>
{
    if (!string.IsNullOrWhiteSpace(autoMapperLicenseKey))
    {
        configuration.LicenseKey = autoMapperLicenseKey;
    }
}, typeof(ApplicationMappingProfile).Assembly, typeof(WebMappingProfile).Assembly);

builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// ======================
// DEPENDENCY INJECTION
// ======================

// Product
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAdminProductRepository, AdminProductRepository>();

// Category
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Admin
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IAdminDashboardRepository, AdminDashboardRepository>();

var app = builder.Build();

// ======================
// PIPELINE
// ======================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/Admin/Dashboard"));
app.MapGet("/Admin", () => Results.Redirect("/Admin/Dashboard"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminDashboard}/{action=Index}/{id?}"
);

app.Run();
