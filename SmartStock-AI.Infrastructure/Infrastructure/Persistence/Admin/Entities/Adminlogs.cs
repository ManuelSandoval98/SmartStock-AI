using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;

public partial class Adminlogs
{
    public int Id { get; set; }

    public string? CorreoAdmin { get; set; }

    public string? Accion { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Negocios? CorreoAdminNavigation { get; set; }
}
