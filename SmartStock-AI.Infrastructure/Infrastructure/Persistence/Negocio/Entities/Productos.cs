using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Productos
{
    public int Id { get; set; }

    public string? CodProducto { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Stock { get; set; }

    public int? Umbral { get; set; }

    public int? IdCategoria { get; set; }

    public double? PrecioVenta { get; set; }

    public double? PrecioCompra { get; set; }

    public double? PrecioDescuento { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public DateTime? FechaEgreso { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categorias? IdCategoriaNavigation { get; set; }

    public virtual ICollection<IngresosProducto> IngresosProducto { get; set; } = new List<IngresosProducto>();

    public virtual ICollection<MovimientosInventario> MovimientosInventario { get; set; } = new List<MovimientosInventario>();
}
