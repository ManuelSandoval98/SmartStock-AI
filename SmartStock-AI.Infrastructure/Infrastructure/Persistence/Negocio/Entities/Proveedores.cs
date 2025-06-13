using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Proveedores
{
    public int Id { get; set; }

    public string? NombreEmpresa { get; set; }

    public string? Ruc { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<IngresosProducto> IngresosProducto { get; set; } = new List<IngresosProducto>();
}
