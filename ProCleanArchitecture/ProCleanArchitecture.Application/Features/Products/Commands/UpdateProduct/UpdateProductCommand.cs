using AutoMapper;
using FluentValidation;
using MediatR;
using ProCleanArchitecture.Domain.Enums;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
    public ProductType Type { get; set; }
    public Guid CategoryId { get; set; }
}

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
        RuleFor(product => product.Name).NotEmpty().MaximumLength(200);
        RuleFor(product => product.Description).MaximumLength(1000);
        RuleFor(product => product.Price).GreaterThanOrEqualTo(0);
        RuleFor(product => product.CategoryId).NotEmpty();
        RuleFor(product => product.Status).IsInEnum();
        RuleFor(product => product.Type).IsInEnum();
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IAdminProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IAdminProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            return false;
        }

        _mapper.Map(request, product);
        await _productRepository.UpdateAsync(product);
        return true;
    }
}
