using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Clientes
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public string? Direccion { get; set; }
}
