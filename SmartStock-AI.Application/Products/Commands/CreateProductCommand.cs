using MediatR;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Domain.Products.Entities;

namespace SmartStock_AI.Application.Products.Commands;

public record CreateProductCommand(
    string CodProducto,
    string Nombre,
    string Descripcion,
    int Stock,
    int Umbral,
    int IdCategoria,
    decimal PrecioVenta,
    decimal PrecioCompra,
    decimal PrecioDescuento,
    DateTime? FechaIngreso,
    DateTime? FechaEgreso,
    DateTime? FechaExpiracion): IRequest<int>;
    
public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var producto = new Producto
        {
            CodProducto = request.CodProducto,
            Nombre = request.Nombre,
            Descripcion = request.Descripcion,
            Stock = request.Stock,
            Umbral = request.Umbral,
            IdCategoria = request.IdCategoria,
            PrecioVenta = request.PrecioVenta,
            PrecioCompra = request.PrecioCompra,
            PrecioDescuento = request.PrecioDescuento,
            FechaIngreso = request.FechaIngreso?.ToUniversalTime(),
            FechaEgreso = request.FechaEgreso?.ToUniversalTime(),
            FechaExpiracion = request.FechaExpiracion?.ToUniversalTime()
        };

        await _unitOfWork.ProductRepository.AddAsync(producto);
        await _unitOfWork.SaveChangesAsync();

        return producto.Id;
    }
}