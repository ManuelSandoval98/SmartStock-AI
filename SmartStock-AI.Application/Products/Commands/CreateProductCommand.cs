using MediatR;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Negocio;
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
    
public class CreateProductHandler(INegocioUnitOfWork _negocioUnitOfWork) : IRequestHandler<CreateProductCommand, int>
{
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
            FechaIngreso = request.FechaIngreso.HasValue?DateTime.SpecifyKind(request.FechaIngreso.Value, DateTimeKind.Unspecified)
                : null,
            FechaEgreso = request.FechaEgreso.HasValue
                ? DateTime.SpecifyKind(request.FechaEgreso.Value, DateTimeKind.Unspecified)
                : null,
            FechaExpiracion = request.FechaExpiracion.HasValue
                ? DateTime.SpecifyKind(request.FechaExpiracion.Value, DateTimeKind.Unspecified)
                : null,
        };

        await _negocioUnitOfWork.ProductRepository.AddAsync(producto);
        await _negocioUnitOfWork.SaveChangesAsync();

        return producto.Id;
    }
}