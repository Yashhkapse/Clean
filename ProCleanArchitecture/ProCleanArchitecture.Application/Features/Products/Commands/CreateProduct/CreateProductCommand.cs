using AutoMapper;
using FluentValidation;
using MediatR;
using ProCleanArchitecture.Domain.Entities;
using ProCleanArchitecture.Domain.Enums;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; } = ProductStatus.Active;
    public ProductType Type { get; set; } = ProductType.Physical;
    public Guid CategoryId { get; set; }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Name).NotEmpty().MaximumLength(200);
        RuleFor(product => product.Description).MaximumLength(1000);
        RuleFor(product => product.Price).GreaterThanOrEqualTo(0);
        RuleFor(product => product.CategoryId).NotEmpty();
        RuleFor(product => product.Status).IsInEnum();
        RuleFor(product => product.Type).IsInEnum();
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IAdminProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IAdminProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        product.Id = Guid.NewGuid();

        await _productRepository.AddAsync(product);
        return product.Id;
    }
}
