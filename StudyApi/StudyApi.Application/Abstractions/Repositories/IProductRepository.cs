using StudyApi.Domain.Entities;

namespace StudyApi.Application.Abstractions.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct);
    Task<Guid> CreateAsync(Product product, CancellationToken ct);
    Task<bool> UpdateAsync(Product product, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}

