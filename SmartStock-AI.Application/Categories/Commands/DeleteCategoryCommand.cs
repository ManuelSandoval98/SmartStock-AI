using MediatR;
using SmartStock_AI.Application.Categories.DTOs;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Negocio;

namespace SmartStock_AI.Application.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<Unit>;

public class DeleteCategoryHandler(INegocioUnitOfWork _negocioUnitOfWork) : IRequestHandler<DeleteCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        // Obtener la categoría existente
        var categoria = await _negocioUnitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (categoria == null)
            throw new KeyNotFoundException("Categoría no encontrada.");

        // Eliminar la categoría
        _negocioUnitOfWork.CategoryRepository.Delete(categoria);
        await _negocioUnitOfWork.SaveChangesAsync();

        return Unit.Value; // Devuelve Unit para indicar éxito sin datos adicionales
    }
}
