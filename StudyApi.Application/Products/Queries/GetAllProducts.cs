using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.Products.Models;

namespace StudyApi.Application.Products.Queries;

public record GetAllProductsQuery() : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsHandler(IProductRepository repo) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repo.GetAllAsync(cancellationToken);
        return entities.Select(e => new ProductDto(e.Id, e.Nome, e.Price, e.CreateDate, e.UpdateDate, e.IsEnabled));
    }
}


