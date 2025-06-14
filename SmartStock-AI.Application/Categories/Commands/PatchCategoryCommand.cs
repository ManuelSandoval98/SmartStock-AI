using MediatR;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Negocio;

namespace SmartStock_AI.Application.Categories.Commands;

public record PatchCategoryCommand(int Id, string Nombre) : IRequest<CategoryDto>;

public class PatchCategoryHandler(INegocioUnitOfWork _negocioUnitOfWork) : IRequestHandler<PatchCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(PatchCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoria = await _negocioUnitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (categoria == null)
            throw new KeyNotFoundException($"La categoría con ID {request.Id} no existe.");

        categoria.Nombre = request.Nombre;
        await _negocioUnitOfWork.SaveChangesAsync();

        return new CategoryDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre
        };
    }
}