using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class MovimientosInventario
{
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public string? TipoMovimiento { get; set; }

    public int? Cantidad { get; set; }

    public DateTime? FechaMovimiento { get; set; }

    public string? Observacion { get; set; }

    public virtual Productos? IdProductoNavigation { get; set; }
}
