using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;

public partial class NegocioLoginTracking
{
    public int Id { get; set; }

    public string CorreoAdmin { get; set; } = null!;

    public int? IntentosFallidos { get; set; }

    public DateTime? BloqueadoHasta { get; set; }
}
