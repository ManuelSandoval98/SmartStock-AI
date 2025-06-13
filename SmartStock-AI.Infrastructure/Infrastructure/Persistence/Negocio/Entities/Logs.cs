using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Logs
{
    public int Id { get; set; }

    public string? Usuario { get; set; }

    public string? Accion { get; set; }

    public string? TablaAfectada { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Usuarios? UsuarioNavigation { get; set; }
}
