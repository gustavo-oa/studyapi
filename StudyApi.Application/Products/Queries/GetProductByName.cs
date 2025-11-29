using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.Products.Models;

namespace StudyApi.Application.Products.Queries;

public record GetProductByNameQuery(String name) : IRequest<ProductDto?>;

public class GetProductByNameHandler(IProductRepository repo) : IRequestHandler<GetProductByNameQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var e = await repo.GetByNameAsync(request.name, cancellationToken);
        return e is null ? null : new ProductDto(e.Id, e.Nome, e.Price, e.CreateDate, e.UpdateDate, e.IsEnabled);
    }
}

