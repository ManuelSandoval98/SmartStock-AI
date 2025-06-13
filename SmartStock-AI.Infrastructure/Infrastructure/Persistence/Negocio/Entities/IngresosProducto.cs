using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class IngresosProducto
{
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public int? IdProveedor { get; set; }

    public int? Cantidad { get; set; }

    public double? PrecioCompra { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? Observacion { get; set; }

    public virtual Productos? IdProductoNavigation { get; set; }

    public virtual Proveedores? IdProveedorNavigation { get; set; }
}
