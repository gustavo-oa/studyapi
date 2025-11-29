using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.Products.Models;

namespace StudyApi.Application.Products.Queries;

public record GetAllEnableProductsQuery() : IRequest<IEnumerable<ProductDto>>;

public class GetAllProductsEnableHandler(IProductRepository repo) : IRequestHandler<GetAllEnableProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<IEnumerable<ProductDto>> Handle(GetAllEnableProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repo.GetAllEnableAsync(cancellationToken);
        return entities.Select(e => new ProductDto(e.Id, e.Nome, e.Price, e.CreateDate, e.UpdateDate, e.IsEnabled));
    }
}
