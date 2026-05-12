using AutoMapper;
using MediatR;
using ProCleanArchitecture.Application.Features.Products.Dtos;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IAdminProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IAdminProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        return product is null ? null : _mapper.Map<ProductDto>(product);
    }
}
