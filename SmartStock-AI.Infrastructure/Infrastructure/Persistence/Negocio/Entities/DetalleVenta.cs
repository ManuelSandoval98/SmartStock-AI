using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class DetalleVenta
{
    public int Id { get; set; }

    public int? IdVenta { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public double? PrecioUnitario { get; set; }

    public double? DescuentoAplicado { get; set; }

    public virtual Productos? IdProductoNavigation { get; set; }

    public virtual Ventas? IdVentaNavigation { get; set; }
}
