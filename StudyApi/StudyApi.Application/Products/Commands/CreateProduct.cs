using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.Products.Models;
using StudyApi.Domain.Entities;

namespace StudyApi.Application.Products.Commands;

public record CreateProductCommand(string Nome, decimal Price, bool? IsEnabled = null) : IRequest<ProductDto>;

public class CreateProductHandler(IProductRepository repo) : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Price = request.Price,
            IsEnabled = request.IsEnabled ?? true,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow
        };

        var id = await repo.CreateAsync(entity, cancellationToken);
        entity.Id = id;
        return new ProductDto(entity.Id, entity.Nome, entity.Price, entity.CreateDate, entity.UpdateDate, entity.IsEnabled);
    }
}

