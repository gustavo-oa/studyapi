using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.OrderNumbers.Models;

namespace StudyApi.Application.OrderNumbers.Queries;

public record GetOrderNumberByIdQuery(Guid Id) : IRequest<OrderNumberDto?>;

public class GetOrderNumberByIdHandler(IOrderNumberRepository repo) : IRequestHandler<GetOrderNumberByIdQuery, OrderNumberDto?>
{
    public async Task<OrderNumberDto?> Handle(GetOrderNumberByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repo.GetByIdAsync(request.Id, cancellationToken);
        return e is null ? null : new OrderNumberDto(e.Id, e.Nome, e.CreateDate, e.UpdateDate, e.IsEnabled);
    }
}

