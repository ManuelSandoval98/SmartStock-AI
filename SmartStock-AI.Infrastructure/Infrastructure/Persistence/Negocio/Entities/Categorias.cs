using System;
using System.Collections.Generic;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

public partial class Categorias
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Productos> Productos { get; set; } = new List<Productos>();
}
