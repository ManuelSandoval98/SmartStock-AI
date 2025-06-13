using MediatR;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.UnitOfWork;

namespace SmartStock_AI.Application.Categories.Commands;

public record PatchCategoryCommand(int Id, string Nombre) : IRequest<CategoryDto>;

public class PatchCategoryHandler : IRequestHandler<PatchCategoryCommand, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public PatchCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Handle(PatchCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoria = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (categoria == null)
            throw new KeyNotFoundException($"La categoría con ID {request.Id} no existe.");

        categoria.Nombre = request.Nombre;
        await _unitOfWork.SaveChangesAsync();

        return new CategoryDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre
        };
    }
}