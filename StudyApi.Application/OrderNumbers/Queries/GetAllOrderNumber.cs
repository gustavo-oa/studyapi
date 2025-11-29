using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.OrderNumbers.Models;

namespace StudyApi.Application.OrderNumbers.Queries;

public record GetAllOrderNumberQuery() : IRequest<IEnumerable<OrderNumberDto>>;

public class GetAllOrderNumberHandler(IOrderNumberRepository repo) : IRequestHandler<GetAllOrderNumberQuery, IEnumerable<OrderNumberDto>>
{
    public async Task<IEnumerable<OrderNumberDto>> Handle(GetAllOrderNumberQuery request, CancellationToken cancellationToken)
    {
        var entities = await repo.GetAllAsync(cancellationToken);
        return entities.Select(e => new OrderNumberDto(e.Id, e.Nome, e.CreateDate, e.UpdateDate, e.IsEnabled));
    }
}


