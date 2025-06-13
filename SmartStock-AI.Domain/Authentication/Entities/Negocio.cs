using System.ComponentModel.DataAnnotations.Schema;

namespace SmartStock_AI.Domain.Authentication.Entities;

[Table("negocios")]
public class Negocio
{
    [Column("id")]
    public int Id { get; set; }
    [Column("nombre_comercial")]
    public string NombreComercial { get; set; }
    [Column("correo_admin")]
    public string CorreoAdmin { get; set; }
    [Column("password_hash")]
    public string PasswordHash { get; set; }
    [Column("db_asociada")]
    public string DbAsociada { get; set; }
    [Column("estado")]
    public bool Estado { get; set; }
    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; }
    [Column("intentosfallidos")]
    public int IntentosFallidos { get; set; } = 0;
    [Column("bloqueadohasta")]
    public DateTime? BloqueadoHasta { get; set; }
}