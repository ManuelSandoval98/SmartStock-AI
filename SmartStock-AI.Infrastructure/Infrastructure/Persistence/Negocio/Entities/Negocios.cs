using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Negocios
{
    public int Id { get; set; }

    public string? NombreComercial { get; set; }

    public string? Ruc { get; set; }

    public string? Direccion { get; set; }

    public string? LogoUrl { get; set; }

    public string? UsuarioAdmin { get; set; }

    public virtual Usuarios? UsuarioAdminNavigation { get; set; }
}
