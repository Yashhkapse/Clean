using AutoMapper;
using MediatR;
using ProCleanArchitecture.Application.Common.Models;
using ProCleanArchitecture.Application.Features.Products.Dtos;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Products.Queries.GetProducts;

public record GetProductsQuery(string? SearchTerm, int PageNumber = 1, int PageSize = 10) : IRequest<PagedResult<ProductDto>>;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PagedResult<ProductDto>>
{
    private readonly IAdminProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IAdminProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = Math.Max(request.PageNumber, 1);
        var pageSize = Math.Clamp(request.PageSize, 5, 50);
        var (items, totalCount) = await _productRepository.GetPagedAsync(request.SearchTerm, pageNumber, pageSize);
        var mappedItems = _mapper.Map<IReadOnlyList<ProductDto>>(items);

        return new PagedResult<ProductDto>(mappedItems, totalCount, pageNumber, pageSize, request.SearchTerm);
    }
}
