using MediatR;
using SmartStock_AI.Application.Products.DTOs;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Negocio;

namespace SmartStock_AI.Application.Products.Queries;

public record GetAllProductsQuery() : IRequest<List<ProductDto>>;

public class GetAllProductsHandler(INegocioUnitOfWork _negocioUnitOfWork) : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
{
    public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productos = await _negocioUnitOfWork.ProductRepository.GetAllWithCategoryAsync();

        var productDtos = productos.Select(p => new ProductDto
        {
            Id = p.Id,
            CodProducto = p.CodProducto,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Stock = p.Stock,
            Umbral = p.Umbral,
            IdCategoria = p.IdCategoria,
            NombreCategoria = p.Categoria?.Nombre ?? "Sin categoría", // 🚀
            PrecioVenta = p.PrecioVenta,
            PrecioCompra = p.PrecioCompra,
            PrecioDescuento = p.PrecioDescuento,
            FechaIngreso = p.FechaIngreso,
            FechaEgreso = p.FechaEgreso,
            FechaExpiracion = p.FechaExpiracion
        }).ToList();

        return productDtos;
    }
}