using MediatR;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Negocio;

namespace SmartStock_AI.Application.Products.Commands;

public record UpdateProductCommand(int Id,
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
    DateTime? FechaExpiracion) : IRequest<Unit>;
public class UpdateProductHandler(INegocioUnitOfWork _negocioUnitOfWork) : IRequestHandler<UpdateProductCommand, Unit>
{
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var producto = await _negocioUnitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (producto == null)
            throw new Exception("Producto no encontrado.");

        producto.CodProducto = request.CodProducto;
        producto.Nombre = request.Nombre;
        producto.Descripcion = request.Descripcion;
        producto.Stock = request.Stock;
        producto.Umbral = request.Umbral;
        producto.IdCategoria = request.IdCategoria;
        producto.PrecioVenta = request.PrecioVenta;
        producto.PrecioCompra = request.PrecioCompra;
        producto.PrecioDescuento = request.PrecioDescuento;
        producto.FechaIngreso = request.FechaIngreso?.ToUniversalTime();
        producto.FechaEgreso = request.FechaEgreso?.ToUniversalTime();
        producto.FechaExpiracion = request.FechaExpiracion?.ToUniversalTime();

        await _negocioUnitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}