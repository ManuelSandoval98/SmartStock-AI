using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Ventas
{
    public int Id { get; set; }

    public string? IdUsuario { get; set; }

    public DateTime? FechaVenta { get; set; }

    public double? TotalVenta { get; set; }

    public string? MetodoPago { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Usuarios? IdUsuarioNavigation { get; set; }
}
