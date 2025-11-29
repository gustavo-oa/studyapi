using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyApi.Application.OrderNumbers.Commands;
using StudyApi.Application.OrderNumbers.Models;
using StudyApi.Application.OrderNumbers.Queries;

namespace StudyApi.Api.Controllers;

//Teste Git//

[ApiController]
[Route("api/[controller]")]
public class OrderNumberController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderNumberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderNumberDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var items = await _mediator.Send(new GetAllOrderNumberQuery(), ct);
        return Ok(items);
    }

    [HttpGet("GetAllEnable")]
    [ProducesResponseType(typeof(IEnumerable<OrderNumberDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllEnable(CancellationToken ct)
    {
        var items = await _mediator.Send(new GetAllEnableOrderNumberQuery(), ct);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(OrderNumberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var item = await _mediator.Send(new GetOrderNumberByIdQuery(id), ct);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("ByName/{name}")]
    [ProducesResponseType(typeof(OrderNumberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByName(string name, CancellationToken ct)
    {
        var item = await _mediator.Send(new GetOrderNumberByNameQuery(name), ct);
        return item is null ? NotFound() : Ok(item);
    }

    // POST, PUT, DELETE continuam iguais, substituindo mediator por _mediator
}
