using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;

public partial class Negocios
{
    public int Id { get; set; }

    public string NombreComercial { get; set; } = null!;

    public string CorreoAdmin { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string DbAsociada { get; set; } = null!;

    public bool? Estado { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? Intentosfallidos { get; set; }

    public DateTime? Bloqueadohasta { get; set; }

    public virtual ICollection<Adminlogs> Adminlogs { get; set; } = new List<Adminlogs>();

    public virtual ICollection<Sesiones> Sesiones { get; set; } = new List<Sesiones>();
}
