using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.OrderNumbers.Models; // ajuste o namespace da pasta Models
using StudyApi.Domain.Entities;



namespace StudyApi.Application.OrderNumbers.Queries // renomeie para OrderNumbers
{
    public record GetAllEnableOrderNumberQuery() : IRequest<IEnumerable<OrderNumberDto>>;

    public class GetAllOrderNumberEnableHandler : IRequestHandler<GetAllEnableOrderNumberQuery, IEnumerable<OrderNumberDto>>
    {
        private readonly IOrderNumberRepository _repo;

        public GetAllOrderNumberEnableHandler(IOrderNumberRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<OrderNumberDto>> Handle(GetAllEnableOrderNumberQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.GetAllEnableAsync(cancellationToken);

            return entities.Select(e => new OrderNumberDto(
                e.Id,
                e.Nome,
                e.CreateDate,
                e.UpdateDate,
                e.IsEnabled
            ));
        }
    }
}
