using MediatR;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.UnitOfWork;

namespace SmartStock_AI.Application.Categories.Commands;

public record UpdateCategoryCommand(int Id, string Nombre) : IRequest<CategoryDto>;

public class UpdateCategoryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Obtener la categoría existente
        var categoria = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (categoria == null)
            throw new KeyNotFoundException("Categoría no encontrada.");

        // Actualizar la entidad
        categoria.Nombre = request.Nombre;

        _unitOfWork.CategoryRepository.Update(categoria);
        await _unitOfWork.SaveChangesAsync();

        // Devolver DTO
        return new CategoryDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre
        };
    }
}