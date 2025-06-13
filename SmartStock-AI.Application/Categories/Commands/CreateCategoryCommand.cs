using MediatR;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Domain.Categories.Entities;

namespace SmartStock_AI.Application.Categories.Commands;

public record CreateCategoryCommand(string Nombre) : IRequest<CategoryDto>;

public class CreateCategoryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Crear entidad
        var nuevaCategoria = new Categoria
        {
            Nombre = request.Nombre
        };

        // Guardar usando UnitOfWork
        await _unitOfWork.CategoryRepository.AddAsync(nuevaCategoria);
        await _unitOfWork.SaveChangesAsync();

        // Retornar DTO
        return new CategoryDto
        {
            Id = nuevaCategoria.Id,
            Nombre = nuevaCategoria.Nombre
        };
    }
}