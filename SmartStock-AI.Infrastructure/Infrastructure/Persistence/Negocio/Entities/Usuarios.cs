using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Usuarios
{
    public string Dni { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Direccion { get; set; }

    public string? Celular { get; set; }

    public string? RazonSocial { get; set; }

    public string? Correo { get; set; }

    public string? Contrasena { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? IdRol { get; set; }

    public virtual Roles? IdRolNavigation { get; set; }

    public virtual ICollection<Logs> Logs { get; set; } = new List<Logs>();

    public virtual ICollection<Negocios> Negocios { get; set; } = new List<Negocios>();

    public virtual ICollection<Ventas> Ventas { get; set; } = new List<Ventas>();
}
