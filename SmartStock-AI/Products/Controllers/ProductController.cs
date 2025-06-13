using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStock_AI.Application.Products.Commands;
using SmartStock_AI.Application.Products.Queries;
using SmartStock_AI.Application.Products.Requests;

namespace SmartStock_AI.Products.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var productos = await _mediator.Send(new GetAllProductsQuery());
        return Ok(productos);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id); // solo devuelve el Id del nuevo producto
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id) return BadRequest("El ID de la ruta no coincide.");
        await _mediator.Send(command);
        return NoContent();
    }
    
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Patch(int id, [FromBody] PatchProductCommand command)
    {
        await _mediator.Send(new PatchProductRequest(id, command));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProductCommand(id));
        return NoContent();
    }
}