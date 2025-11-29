using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.Products.Models;

namespace StudyApi.Application.Products.Commands;

public record UpdateProductCommand(Guid Id, string Nome, decimal Price, bool IsEnabled) : IRequest<ProductDto?>;

public class UpdateProductHandler(IProductRepository repo) : IRequestHandler<UpdateProductCommand, ProductDto?>
{
    public async Task<ProductDto?> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await repo.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) return null;

        existing.Nome = request.Nome;
        existing.Price = request.Price;
        existing.IsEnabled = request.IsEnabled;
        existing.UpdateDate = DateTime.UtcNow;

        var ok = await repo.UpdateAsync(existing, cancellationToken);
        if (!ok) return null;
        return new ProductDto(existing.Id, existing.Nome, existing.Price, existing.CreateDate, existing.UpdateDate, existing.IsEnabled);
    }
}

