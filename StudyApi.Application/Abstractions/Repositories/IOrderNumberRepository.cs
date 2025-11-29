using StudyApi.Domain.Entities;

namespace StudyApi.Application.Abstractions.Repositories;

public interface IOrderNumberRepository
{
    Task<OrderNumber?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<OrderNumber?> GetByNameAsync(String name, CancellationToken ct);
    Task<IEnumerable<OrderNumber>> GetAllAsync(CancellationToken ct);
    Task<IEnumerable<OrderNumber>> GetAllEnableAsync(CancellationToken ct);
    Task<Guid> CreateAsync(OrderNumber ordernumber, CancellationToken ct);
    Task<bool> UpdateAsync(OrderNumber ordernumber, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}



