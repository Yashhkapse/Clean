using AutoMapper;
using MediatR;
using ProCleanArchitecture.Application.Features.Categories.Dtos;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Categories.Queries.GetCategoryLookup;

public record GetCategoryLookupQuery : IRequest<IReadOnlyList<CategoryLookupDto>>;

public class GetCategoryLookupQueryHandler : IRequestHandler<GetCategoryLookupQuery, IReadOnlyList<CategoryLookupDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryLookupQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<CategoryLookupDto>> Handle(GetCategoryLookupQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<CategoryLookupDto>>(categories.OrderBy(category => category.Name));
    }
}
