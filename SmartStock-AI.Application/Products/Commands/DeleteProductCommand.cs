using MediatR;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Negocio;

namespace SmartStock_AI.Application.Products.Commands;

public record DeleteProductCommand(int Id): IRequest<Unit>;

public class DeleteProductHandler(INegocioUnitOfWork _negocioUnitOfWork) : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var producto = await _negocioUnitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (producto == null)
            throw new Exception("Producto no encontrado.");

        _negocioUnitOfWork.ProductRepository.Delete(producto);
        await _negocioUnitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
