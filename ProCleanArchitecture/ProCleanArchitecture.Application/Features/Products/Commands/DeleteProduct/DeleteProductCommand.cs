using MediatR;
using ProCleanArchitecture.Domain.Interfaces;

namespace ProCleanArchitecture.Application.Features.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IAdminProductRepository _productRepository;

    public DeleteProductCommandHandler(IAdminProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product is null)
        {
            return false;
        }

        await _productRepository.DeleteAsync(product);
        return true;
    }
}
