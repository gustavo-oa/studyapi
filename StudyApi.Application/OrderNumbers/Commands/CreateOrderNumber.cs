using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.OrderNumbers.Models;
using StudyApi.Domain.Entities;

namespace StudyApi.Application.OrderNumbers.Commands; // ✅ Namespace correto com "OrderNumbers"

/// <summary>
/// Command: carrega os dados para criar um número de ordem
/// </summary>
public record CreateOrderNumberCommand(
    string Nome,
    decimal Price,
    bool? IsEnabled = null
) : IRequest<OrderNumberDto>;

/// <summary>
/// Handler: lógica de criação do número de ordem
/// </summary>
public class CreateOrderNumberHandler : IRequestHandler<CreateOrderNumberCommand, OrderNumberDto>
{
    private readonly IOrderNumberRepository _repo;

    public CreateOrderNumberHandler(IOrderNumberRepository repo)
    {
        _repo = repo;
    }

    public async Task<OrderNumberDto> Handle(CreateOrderNumberCommand request, CancellationToken cancellationToken)
    {
        // 1️⃣ Validação de duplicidade
        var existing = await _repo.GetByNameAsync(request.Nome, cancellationToken);
        if (existing != null)
            throw new InvalidOperationException("Já existe um número de ordem com esse nome.");

        // 2️⃣ Criação do número de ordem
        var entity = new OrderNumber
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            IsEnabled = request.IsEnabled ?? true
        };

        var id = await _repo.CreateAsync(entity, cancellationToken);
        entity.Id = id;

        // 3️⃣ Retorna DTO
        return new OrderNumberDto(
            entity.Id,
            entity.Nome,
            entity.CreateDate,
            entity.UpdateDate,
            entity.IsEnabled
        );
    }
}
