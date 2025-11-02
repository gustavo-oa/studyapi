using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Application.Products.Commands;
using StudyApi.Application.Products.Models;
using StudyApi.Application.Products.Queries;

namespace StudyApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await mediator.Send(new GetAllProductsQuery(), ct);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var item = await mediator.Send(new GetProductByIdQuery(id), ct);
        return item is null ? NotFound() : Ok(item);
    }

    public record CreateProductRequest(string Nome, decimal Price, bool? IsEnabled);

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken ct)
    {
        var created = await mediator.Send(new CreateProductCommand(request.Nome, request.Price, request.IsEnabled), ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    public record UpdateProductRequest(string Nome, decimal Price, bool IsEnabled);

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken ct)
    {
        var updated = await mediator.Send(new UpdateProductCommand(id, request.Nome, request.Price, request.IsEnabled), ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteProductCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }
}

