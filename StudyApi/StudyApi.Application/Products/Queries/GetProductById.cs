using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.Products.Models;

namespace StudyApi.Application.Products.Queries;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto?>;

public class GetProductByIdHandler(IProductRepository repo) : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repo.GetByIdAsync(request.Id, cancellationToken);
        return e is null ? null : new ProductDto(e.Id, e.Nome, e.Price, e.CreateDate, e.UpdateDate, e.IsEnabled);
    }
}

