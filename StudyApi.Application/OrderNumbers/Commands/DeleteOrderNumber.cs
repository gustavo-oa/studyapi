using MediatR;
using StudyApi.Application.Abstractions.Repositories;

namespace StudyApi.Application.OrderNumbers.Commands; // ✅ Namespace correto

/// <summary>
/// Command para deletar um número de ordem
/// </summary>
public record DeleteOrderNumberCommand(Guid Id) : IRequest<bool>;

/// <summary>
/// Handler: lógica de deleção do número de ordem
/// </summary>
public class DeleteOrderNumberHandler : IRequestHandler<DeleteOrderNumberCommand, bool>
{
    private readonly IOrderNumberRepository _repo;

    public DeleteOrderNumberHandler(IOrderNumberRepository repo)
    {
        _repo = repo;
    }

    public Task<bool> Handle(DeleteOrderNumberCommand request, CancellationToken cancellationToken)
    {
        return _repo.DeleteAsync(request.Id, cancellationToken);
    }
}
