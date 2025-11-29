using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.OrderNumbers.Models;

namespace StudyApi.Application.OrderNumbers.Queries;

public record GetOrderNumberByNameQuery(String name) : IRequest<OrderNumberDto?>;

public class GetOrderNumberByNameHandler(IOrderNumberRepository repo) : IRequestHandler<GetOrderNumberByNameQuery, OrderNumberDto?>
{
    public async Task<OrderNumberDto?> Handle(GetOrderNumberByNameQuery request, CancellationToken cancellationToken)
    {
        var e = await repo.GetByNameAsync(request.name, cancellationToken);
        return e is null ? null : new OrderNumberDto(e.Id, e.Nome, e.CreateDate, e.UpdateDate, e.IsEnabled);
    }
}

