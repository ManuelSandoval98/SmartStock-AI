using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartStock_AI.Application.Categories.Commands;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.Categories.Queries;

namespace SmartStock_AI.Categories.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/category
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var query = new GetAllCategoriesQuery();
        var categorias = await _mediator.Send(query);
        return Ok(categorias);
    }

    // POST: api/category
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryCommand command)
    {
        var nuevaCategoria = await _mediator.Send(command);
        return Ok(nuevaCategoria);
    }

    // PUT: api/category/{id}
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] UpdateCategoryCommand command)
    {
        if (id != command.Id)
            return BadRequest(new { message = "El id de la URL y del cuerpo no coinciden." });

        var categoriaActualizada = await _mediator.Send(command);
        return Ok(categoriaActualizada);
    }
    
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<IActionResult> Patch(int id, [FromBody] string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return BadRequest("El nombre no puede ser vacío.");

        var result = await _mediator.Send(new PatchCategoryCommand(id, nombre));
        return Ok(result);
    }

    // DELETE: api/category/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteCategoryCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}