using MediatR;
using SmartStock_AI.Application.Products.Requests;
using SmartStock_AI.Application.UnitOfWork;

namespace SmartStock_AI.Application.Products.Commands;

public record PatchProductCommand(
    string? Nombre,
    string? Descripcion,
    int? Stock,
    int? Umbral,
    int? IdCategoria,
    decimal? PrecioVenta,
    decimal? PrecioCompra,
    decimal? PrecioDescuento,
    DateTime? FechaIngreso,
    DateTime? FechaEgreso,
    DateTime? FechaExpiracion
) : IRequest<Unit>;

public class PatchProductHandler : IRequestHandler<PatchProductRequest, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public PatchProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(PatchProductRequest request, CancellationToken cancellationToken)
    {
        var producto = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);
        if (producto == null)
            throw new KeyNotFoundException("Producto no encontrado");

        var data = request.Command;

        if (data.Nombre is not null) producto.Nombre = data.Nombre;
        if (data.Descripcion is not null) producto.Descripcion = data.Descripcion;
        if (data.Stock.HasValue) producto.Stock = data.Stock.Value;
        if (data.Umbral.HasValue) producto.Umbral = data.Umbral.Value;
        if (data.IdCategoria.HasValue) producto.IdCategoria = data.IdCategoria.Value;
        if (data.PrecioVenta.HasValue) producto.PrecioVenta = data.PrecioVenta.Value;
        if (data.PrecioCompra.HasValue) producto.PrecioCompra = data.PrecioCompra.Value;
        if (data.PrecioDescuento.HasValue) producto.PrecioDescuento = data.PrecioDescuento.Value;
        if (data.FechaIngreso.HasValue) producto.FechaIngreso = data.FechaIngreso.Value.ToUniversalTime();
        if (data.FechaEgreso.HasValue) producto.FechaEgreso = data.FechaEgreso.Value.ToUniversalTime();
        if (data.FechaExpiracion.HasValue) producto.FechaExpiracion = data.FechaExpiracion.Value.ToUniversalTime();

        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}