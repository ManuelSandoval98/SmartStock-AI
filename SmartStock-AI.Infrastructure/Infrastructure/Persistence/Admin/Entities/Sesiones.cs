using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;

public partial class Sesiones
{
    public int Id { get; set; }

    public string? CorreoAdmin { get; set; }

    public string Token { get; set; } = null!;

    public DateTime? FechaInicio { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public virtual Negocios? CorreoAdminNavigation { get; set; }
}
