using MediatR;
using StudyApi.Application.Abstractions.Repositories;

namespace StudyApi.Application.Products.Commands;

public record DeleteProductCommand(Guid Id) : IRequest<bool>;

public class DeleteProductHandler(IProductRepository repo) : IRequestHandler<DeleteProductCommand, bool>
{
    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        => repo.DeleteAsync(request.Id, cancellationToken);
}

