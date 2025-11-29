using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.OrderNumbers.Models;
using StudyApi.Domain.Entities;

namespace StudyApi.Application.OrderNumbers.Commands; // ✅ padrão plural

/// <summary>
/// Command para atualizar um número de ordem
/// </summary>
public record UpdateOrderNumberCommand(Guid Id, string Nome, bool IsEnabled) : IRequest<OrderNumberDto?>;

/// <summary>
/// Handler: lógica de atualização do número de ordem
/// </summary>
public class UpdateOrderNumberHandler : IRequestHandler<UpdateOrderNumberCommand, OrderNumberDto?>
{
    private readonly IOrderNumberRepository _repo;

    public UpdateOrderNumberHandler(IOrderNumberRepository repo)
    {
        _repo = repo;
    }

    public async Task<OrderNumberDto?> Handle(UpdateOrderNumberCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (existing is null) return null;

        existing.Nome = request.Nome;
        existing.IsEnabled = request.IsEnabled;
        existing.UpdateDate = DateTime.UtcNow;

        var ok = await _repo.UpdateAsync(existing, cancellationToken);
        if (!ok) return null;

        return new OrderNumberDto(
            existing.Id,
            existing.Nome,
            existing.CreateDate,
            existing.UpdateDate,
            existing.IsEnabled
        );
    }
}
