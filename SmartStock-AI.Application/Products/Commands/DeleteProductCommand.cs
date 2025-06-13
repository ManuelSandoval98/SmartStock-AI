using MediatR;
using SmartStock_AI.Application.UnitOfWork;

namespace SmartStock_AI.Application.Products.Commands;

public record DeleteProductCommand(int Id): IRequest<Unit>;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var producto = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (producto == null)
            throw new Exception("Producto no encontrado.");

        _unitOfWork.ProductRepository.Delete(producto);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
